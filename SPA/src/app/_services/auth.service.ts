import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  login( model: any ) {

    return this.http.post( this.baseUrl + 'login', model )
    // When we post the user, the post method returns our token and
    // we need to store the token in out local memory for future access.
      .pipe(
        map(( response: any ) => {

          const user = response;

          if ( user ) {
            localStorage.setItem( 'token', user.token )
          }

        })
       );
  }
}
