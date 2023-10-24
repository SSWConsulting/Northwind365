import { Component } from '@angular/core';
import { AuthenticationResult, Authorizev2Service, NetCore8LoginModel } from '../authorizev2.service';


// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
@Component({
  selector: 'app-login-v2',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginV2Component{

  constructor(private authService: Authorizev2Service) { }

  loginClicked() {

    let username = (<HTMLInputElement>document.getElementById("username")).value;
    let password = (<HTMLInputElement>document.getElementById("password")).value;

    let loginModel: NetCore8LoginModel = {
      email: username,
      password: password
    };

    this.authService.handleLogin(loginModel).subscribe((result: AuthenticationResult) => {
      console.log(result);

      if (result == AuthenticationResult.Success) {
        console.log("Login successful");
        window.location.href = "/";
      } else {
        // handle failure
      }
    });
    
  }
}