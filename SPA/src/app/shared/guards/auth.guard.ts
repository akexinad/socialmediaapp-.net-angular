import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '@services/auth.service';
import { AlertifyService } from '@services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {

  // We want to check 3 things:
  // We want to check if the user is logged in:
  // Be able to redirect the user:
  // Warn the user.

  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService

  ) {}

  canActivate(): boolean {

    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('You shall not pass!!!');
    this.router.navigate(['/home']);
  }

}
