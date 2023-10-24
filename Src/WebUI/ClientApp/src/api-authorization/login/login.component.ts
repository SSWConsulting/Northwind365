import { Component, OnInit } from '@angular/core';
import { AuthenticationResult, AuthorizeService, NetCore8LoginModel } from '../authorize.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationPaths, ReturnUrlType } from '../api-authorization.constants';


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

  // todo: check to see whether the login can be refreshed rather than requiring an interactive login

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

    // todo: show that something is happening
    
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

        let returnUrl = this.getReturnUrl();

        await this.router.navigate([returnUrl]);
      } else {
        // handle failure
      }
    });
    
  }

  getReturnUrl(): string {
    return this.activatedRoute.snapshot.queryParams[ReturnUrlType] || '/';
  }

}
