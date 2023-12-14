import { ChangeDetectionStrategy, Component, effect, inject, OnInit, signal } from '@angular/core';
import { AuthenticationResult, AuthorizeService } from '../authorize.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReturnUrlType } from '../api-authorization.constants';
import { filter, switchMap } from 'rxjs';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent implements OnInit{
  private authService = inject(AuthorizeService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  private toastrService = inject(ToastrService);

  form = this.fb.group({
    username: this.fb.control(''),
    password: this.fb.control(''),
  });

  isBusy = signal(false);
  buttonText: string = 'Login';

  constructor() {
    effect(() => {
      if (this.isBusy()) {
        this.form.disable();
      } else {
        this.form.enable();
      }
    });
  }

  ngOnInit(): void {
    this.authService.refreshLogin().pipe(
      filter(result => result === AuthenticationResult.Success),
      switchMap(() => {
        const returnUrl = this.getReturnUrl();
        return this.router.navigate([returnUrl]);
      })
    ).subscribe();
  }

  loginClicked() {
    this.isBusy.set(true);
    this.buttonText = 'Logging in...';

    const { username, password } = this.form.value;

    this.authService.handleLogin({
      email: username,
      password: password
    }).subscribe((result: AuthenticationResult) => {
      if (result == AuthenticationResult.Success) {
        this.toastrService.success('✅ Login successful! Sending you back where you came from...');

        let returnUrl = this.getReturnUrl();

        this.router.navigate([returnUrl]);
      } else {
        // handle failure
        this.isBusy.set(false);
        this.buttonText = 'Login';
        this.toastrService.error('⚠️ Login failed. Please check your credentials and try again.');
      }
    });

  }

  getReturnUrl(): string {
    return this.activatedRoute.snapshot.queryParams[ReturnUrlType] || '/';
  }
}
