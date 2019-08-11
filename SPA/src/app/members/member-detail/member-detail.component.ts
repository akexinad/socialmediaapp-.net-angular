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
    // FROM THE RESOLVER VIA THE ROUTE
    this.route.data.subscribe( data => {
      this.user = data.userFromResolver;
    });
  }

  // loadUser() {
  //   // the id parameter is accessed via the router link in the member card html component
  //   this.userService.getUser(this.route.snapshot.params.id).subscribe( (user: User) => {
  //     this.user = user;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }

}
