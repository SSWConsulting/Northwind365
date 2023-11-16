import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { AuthorizeService } from '../authorize.service';

// The main responsibility of this component is to handle the user's logout process.
// This is the starting point for the logout process, which is usually initiated when a
// user clicks on the logout button on the LoginMenu component.
@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {
  public message = new BehaviorSubject<string>(null);

  constructor(
    private authorizeService: AuthorizeService,
    private router: Router,
  ) { }

  ngOnInit() {
    this.authorizeService.logout();



    this.router.navigate(
      ["/"],
      {
        queryParams: {
          loggedOut: true,
        },
      });
  }
}
