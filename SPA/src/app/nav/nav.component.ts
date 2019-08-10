import { Component, OnInit } from '@angular/core';

import { AuthService } from '@services/auth.service';
import { AlertifyService } from '@services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(
    // authservice is made public so it is visible within
    // the van html component
    public authService: AuthService,
    private alertify: AlertifyService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  login() {
    this.authService.login( this.model ).subscribe(
      next => {
        this.alertify.success('Logged in successfully');
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.router.navigate(['/members']);
      }
    );
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
