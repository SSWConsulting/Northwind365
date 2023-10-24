import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthorizeService } from '../authorize.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;

  constructor(private authorizeService: AuthorizeService, private userService: UserService) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.getLoggedInState();
    this.userName = this.userService.getUserName();
  }
}
