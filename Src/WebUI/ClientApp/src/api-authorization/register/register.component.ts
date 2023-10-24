import { Component } from '@angular/core';
import { AuthenticationResult, AuthorizeService, NetCore8LoginModel } from '../authorize.service';
import { ApplicationPaths } from '../api-authorization.constants';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private authService: AuthorizeService) { }

  registerClicked() {

    // todo: show that something is happening
    
    let username = (<HTMLInputElement>document.getElementById("username")).value;
    let password = (<HTMLInputElement>document.getElementById("password")).value;

    let registerModel: NetCore8LoginModel = {
      email: username,
      password: password
    };

    this.authService.registerUser(registerModel).subscribe((result: AuthenticationResult) => {
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
