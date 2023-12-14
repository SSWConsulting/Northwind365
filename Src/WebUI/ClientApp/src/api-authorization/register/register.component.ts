import { Component } from '@angular/core';
import { AuthenticationResult, AuthorizeService } from '../authorize.service';
import { ReturnUrlType } from '../api-authorization.constants';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterRequest } from '../../app/northwind-traders-api';

declare var bootstrap: any;

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor(private authService: AuthorizeService, private router: Router, private activatedRoute: ActivatedRoute) { }

  isBusy: boolean = false;

  notificationMessage: string = '';

  buttonText: string = 'Register';

  registerClicked() {

    this.isBusy = true;
    this.buttonText = 'Registering...';

    let username = (<HTMLInputElement>document.getElementById("username")).value;
    let password = (<HTMLInputElement>document.getElementById("password")).value;

    const registerModel: RegisterRequest = {
      email: username,
      password: password
    };

    this.authService.registerUser(registerModel).subscribe((result: AuthenticationResult) => {
      if (result == AuthenticationResult.Success) {

        this.notificationMessage = "✅ Registration successful! Logging you in...";
        this.showToast();

        this.authService.handleLogin(registerModel)
          .subscribe(async (result: AuthenticationResult) => {
            if (result == AuthenticationResult.Success) {
              this.notificationMessage = "✅ Login successful! Redirecting...";
              this.showToast();

              let returnUrl = this.getReturnUrl();

              await this.router.navigate([returnUrl]);

            } else {
              // handle failure
              this.notificationMessage = "⚠️ Could not automatically log you in. Please navigate to the login page.";
              this.showToast();

              this.isBusy = false;
              this.buttonText = 'Register';
            }
        });

      } else {
        // handle failure
        this.notificationMessage = "⚠️ Registration failed. Please try again.";
        this.showToast();

        this.isBusy = false;
        this.buttonText = 'Register';
      }
    });

  }

  getReturnUrl(): string {
    return this.activatedRoute.snapshot.queryParams[ReturnUrlType] || '/';
  }

  showToast() {
    const toastRef = document.getElementById("statusToast");
    const toast = bootstrap.Toast.getOrCreateInstance(toastRef);
    toast.show();
  }

}
