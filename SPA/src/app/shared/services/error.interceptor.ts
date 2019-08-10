import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse, HTTP_INTERCEPTORS
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .pipe(
                catchError(error => {
                    if (error instanceof HttpErrorResponse) {

                        // FOR ERRORS THAT HAVE A PARTICULAR STATUS CODE
                        if (error.status === 401) {
                            return throwError(error.statusText);
                        }

                        // Here we get the errors we created inside the Extensions helper class in our backend.
                        const applicationError = error.headers.get('Application-Error');

                        if (applicationError) {
                            console.error(applicationError);
                            return throwError(applicationError);
                        }

                        // This second part ensures that we handle all the server errors that our API throws to the client.
                        const serverError = error.error.errors || error.error;
                        let modalStateErrors = '';
                        if (serverError && typeof serverError === 'object') {
                            for (const key in serverError) {
                                if (serverError[key]) {
                                    modalStateErrors += serverError[key] + '\n';
                                }
                            }
                        }
                        return throwError(modalStateErrors || serverError || 'Server Error');
                    }
                })
            );
    }
}

// NOTE: THIS IS WHAT WE ADD IN THE APP MODULE.
export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
