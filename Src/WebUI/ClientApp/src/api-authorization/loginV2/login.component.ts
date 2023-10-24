import { Component, Inject, OnInit } from '@angular/core';
import { Authorizev2Service, NetCore8LoginResponse } from '../authorizev2.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { LoginActions, QueryParameterNames, ApplicationPaths, ReturnUrlType } from '../api-authorization.constants';
import { LoginResponse, OidcSecurityService } from 'angular-auth-oidc-client';
import { API_BASE_URL } from 'src/app/northwind-traders-api';

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
@Component({
  selector: 'app-login-v2',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginV2Component implements OnInit {

  constructor(
    private authService: Authorizev2Service,
    @Inject(API_BASE_URL) private baseUrl?: string
     ) { }

  loginClicked() {

    let username = (<HTMLInputElement>document.getElementById("username")).value;
    let password = (<HTMLInputElement>document.getElementById("password")).value;

    let data = {
      email: username,
      password: password
    };

    fetch(`${this.baseUrl}/login`, {
      method: 'POST',
      body: JSON.stringify(data),
      headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      },
      mode: 'cors'
    }).then(response => {
      if (response.ok) {
        return response.json();
      } else {
        throw new Error('Something went wrong');
      }
    }).then((response: NetCore8LoginResponse) => {
      console.log(response);
      this.authService.handleLogin(response);
      window.location.href = "/";
    }).catch((error) => {
      console.log(error);
    });
  }

  ngOnInit() {

  }
}
