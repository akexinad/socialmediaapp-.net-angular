import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AlertifyService } from '@services/alertify.service';

import { User } from '@models/user';
import { NgForm } from '@angular/forms';
import { UserService } from '@services/user.service';
import { AuthService } from '@services/auth.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  user: User;

  // ViewChild is utilized if we want to make changes to the DOM after an event
  @ViewChild('editForm', { static: true }) editForm: NgForm;

  // We can listen for events that happen outside of angular and within the browser.
  // If a user edits the form and accidentally tries closes the browser window,
  // the user will be warned with a browser alert.
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private userService: UserService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.userFromEditResolver;
    });
  }

  updateUser() {
    this.userService.updateUser(this.authService.decodedToken.nameid, this.user)
      .subscribe( next => {
        this.alertify.success('Updated successfully!');
        // Reset the form with information stored in the user model
        this.editForm.reset(this.user);
      }, error => {
        this.alertify.error(error);
      });
  }
}
