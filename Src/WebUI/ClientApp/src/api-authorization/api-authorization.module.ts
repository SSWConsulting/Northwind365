import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { LogoutComponent } from './logout/logout.component';
import { RouterModule } from '@angular/router';
import { ApplicationPaths } from './api-authorization.constants';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(
      [
        { path: ApplicationPaths.Register, component: RegisterComponent },
        { path: ApplicationPaths.Profile, component: LoginComponent },
        { path: ApplicationPaths.Login, component: LoginComponent },
        { path: ApplicationPaths.LoginFailed, component: LoginComponent },
        { path: ApplicationPaths.LoginCallback, component: LoginComponent },
        { path: ApplicationPaths.LogOut, component: LogoutComponent },
        { path: ApplicationPaths.LoggedOut, component: LogoutComponent },
        { path: ApplicationPaths.LogOutCallback, component: LogoutComponent }
      ]
    ),
    ReactiveFormsModule
  ],
  declarations: [LoginMenuComponent, LogoutComponent, LoginComponent, RegisterComponent],
  exports: [LoginMenuComponent, LogoutComponent, LoginComponent]
})
export class ApiAuthorizationModule { }
