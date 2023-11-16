import { Component } from '@angular/core';
import { AuthenticationResult, AuthorizeService, NetCore8LoginModel } from '../authorize.service';
import { ApplicationPaths } from '../api-authorization.constants';
import { Router } from '@angular/router';

declare var bootstrap: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private authService: AuthorizeService, private router: Router) { }

  isBusy: boolean = false;
  
  notificationMessage: string = '';

  buttonText: string = 'Register';

  registerClicked() {

    this.isBusy = true;
    this.buttonText = 'Registering...';

    let username = (<HTMLInputElement>document.getElementById("username")).value;
    let password = (<HTMLInputElement>document.getElementById("password")).value;

    let registerModel: NetCore8LoginModel = {
      email: username,
      password: password
    };

    this.authService.registerUser(registerModel).subscribe((result: AuthenticationResult) => {
      console.log(result);

      if (result == AuthenticationResult.Success) {

        this.notificationMessage = "✅ Registration successful! Logging you in...";
        this.showToast();

        this.authService.handleLogin(registerModel)
          .subscribe((result: AuthenticationResult) => {
            console.log(result);

            if (result == AuthenticationResult.Success) {
              this.notificationMessage = "✅ Login successful! Redirecting...";
              this.showToast();

              this.router.navigate(["/"]); // todo: redirect to previous page?

            } else {
              // handle failure
              console.log("Login failed")
              
              this.notificationMessage = "⚠️ Could not automatically log you in. Please navigate to the login page.";
              this.showToast();

              this.isBusy = false;
              this.buttonText = 'Register';
            }
        });

      } else {
        // handle failure
        console.log("Registration failed")
        
        this.notificationMessage = "⚠️ Registration failed. Please try again.";
        this.showToast();

        this.isBusy = false;
        this.buttonText = 'Register';
      }
    });

  }

  showToast() {
    const toastRef = document.getElementById("statusToast");
    const toast = bootstrap.Toast.getOrCreateInstance(toastRef);
    toast.show();
  }

}
