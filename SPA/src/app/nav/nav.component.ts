import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login( this.model ).subscribe(
      next => {

        console.log('Logged in successfully');

      }, error => {

        throw new Error(error);

      }
    );
  }

  loggedIn() {

    const token = localStorage.getItem('token');

    // This is a short hand for an if-statement
    // If there is a token, return true, else return false.
    return !!token;
  }

  logout() {

    localStorage.removeItem('token');

    console.log('logged out');

  }

}
