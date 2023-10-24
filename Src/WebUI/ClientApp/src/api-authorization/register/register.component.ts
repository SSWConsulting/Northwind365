import { Component } from '@angular/core';
import { AuthenticationResult, Authorizev2Service, NetCore8LoginModel } from '../authorizev2.service';
import { ApplicationPaths } from '../api-authorization.constants';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private authService: Authorizev2Service) { }

  registerClicked() {

    let username = (<HTMLInputElement>document.getElementById("username")).value;
    let password = (<HTMLInputElement>document.getElementById("password")).value;

    let loginModel: NetCore8LoginModel = {
      email: username,
      password: password
    };

    this.authService.handleLogin(loginModel).subscribe((result: AuthenticationResult) => {
      console.log(result);

      if (result == AuthenticationResult.Success) {
        console.log("Registration successful");
        console.log("Redirecting to: " + ApplicationPaths.Login);

        window.location.href = ApplicationPaths.Login;

        // todo: Automatically log the user in, capture additional info from the form, and populate their profile, then redirect home instead of to the login page.

      } else {
        // handle failure
      }
    });
    
  }

}
