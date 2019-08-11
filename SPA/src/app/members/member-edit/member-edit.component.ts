import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AlertifyService } from '@services/alertify.service';

import { User } from '@models/user';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  @ViewChild('editForm', { static: true }) editForm: NgForm;
  user: User;

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.userFromEditResolver;
    });
  }

  updateUser() {
    console.log(this.user);
    this.alertify.success('Updated successfully!');
    // Reset the form with information stored in the user model
    this.editForm.reset(this.user);
  }

}
