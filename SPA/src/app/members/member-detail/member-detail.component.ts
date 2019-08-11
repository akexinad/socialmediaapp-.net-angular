import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '@services/alertify.service';
import { ActivatedRoute } from '@angular/router';

import { UserService } from '@services/user.service';

import { User } from '@models/user';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  user: User;

  constructor(
    private userService: UserService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser() {
    // the id parameter is accessed via the router link in the member card html component
    this.userService.getUser(this.route.snapshot.params.id).subscribe( (user: User) => {
      this.user = user;
    }, error => {
      this.alertify.error(error);
    });
  }

}
