import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

import { AuthService } from '@services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  // As soon as the app runs, we get the token and ensure that it is available
  // globally throughout the app without any errors that can occur due to
  // the nature of js runtime and compilation.

  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService) { }

  ngOnInit() {
    const token = localStorage.getItem('token');

    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
}
