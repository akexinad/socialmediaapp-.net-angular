import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';

import { User } from '@models/user';
import { UserService } from '@services/user.service';
import { AlertifyService } from '@services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

// IMPORTANT
// The resolver solves the problem that we experience when we visit a the member detail component
// where the component loads before the app is able to retrieve the data, thus we have to
// use elvis/safe navigation operators to show the data.

// The resolver is then imported in the app module as a provider,
// and it is attached to the member detail route in our route.ts file.

// Finally, in the ts component, instead of retrieving the data from the service, you retrieve it via the route,
// which is connected to the resolver by subscribing to the route and accessing the property
// whose key is equal to the key provided in the route.ts file.

@Injectable()
export class MemberListResolver implements Resolve<User[]> {

    constructor(
        private userService: UserService,
        private router: Router,
        private alertify: AlertifyService
    ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.userService.getUsers()
            .pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data...' + '\n' + error);
                    this.router.navigate(['/home']);
                    return of(null);
                })
            );
    }
}
