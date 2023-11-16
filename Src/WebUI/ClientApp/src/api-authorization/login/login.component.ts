import { Component, OnInit } from '@angular/core';
import { AuthenticationResult, AuthorizeService, NetCore8LoginModel } from '../authorize.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationPaths, ReturnUrlType } from '../api-authorization.constants';
import {RegisterComponent} from "../register/register.component";

declare var bootstrap: any;

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  constructor(private authService: AuthorizeService, private router: Router, private activatedRoute: ActivatedRoute) { }

  isBusy: boolean = false;
  buttonText: string = 'Login';
  toastMessage: string = '';

  ngOnInit(): void {

    console.log("Login component initialized. Attempting refresh...");

    this.authService.refreshLogin().subscribe(async (result: AuthenticationResult) => {
      if (result === AuthenticationResult.Success) {
        console.log("Login refresh successful on login page");

        let returnUrl = this.getReturnUrl();

        console.log("Redirecting to: " + returnUrl);
        await this.router.navigate([returnUrl]);
      } else {
        console.log("Login refresh failed on login page");
        console.log(result);
      }
    })
  }

  loginClicked() {

    this.isBusy = true;
    this.buttonText = 'Logging in...';

    let username = (<HTMLInputElement>document.getElementById("username")).value;
    let password = (<HTMLInputElement>document.getElementById("password")).value;

    let loginModel: NetCore8LoginModel = {
      email: username,
      password: password
    };

    this.authService.handleLogin(loginModel).subscribe(async (result: AuthenticationResult) => {
      console.log(result);

      if (result == AuthenticationResult.Success) {
        console.log("Login successful");
        this.toastMessage = '✅ Login successful! Sending you back where you came from...';
        this.showToast();

        let returnUrl = this.getReturnUrl();

        await this.router.navigate([returnUrl]);
      } else {
        // handle failure
        this.isBusy = false;
        this.buttonText = 'Login';
        this.toastMessage = '⚠️ Login failed. Please check your credentials and try again.';
        this.showToast();
      }
    });

  }

  getReturnUrl(): string {
    return this.activatedRoute.snapshot.queryParams[ReturnUrlType] || '/';
  }

  showToast() {
    const toastRef = document.getElementById("loginFailedToast");
    const toast = bootstrap.Toast.getOrCreateInstance(toastRef);
    toast.show();
  }

}
