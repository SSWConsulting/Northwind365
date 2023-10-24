import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginMenuComponent } from './login-menu/login-menu.component';
import { LogoutComponent } from './logout/logout.component';
import { RouterModule } from '@angular/router';
import { ApplicationPaths } from './api-authorization.constants';
import { HttpClientModule } from '@angular/common/http';
import { LoginV2Component } from './loginV2/login.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(
      [
        { path: ApplicationPaths.Register, component: RegisterComponent },
        { path: ApplicationPaths.Profile, component: LoginV2Component },
        { path: ApplicationPaths.Login, component: LoginV2Component },
        { path: ApplicationPaths.LoginFailed, component: LoginV2Component },
        { path: ApplicationPaths.LoginCallback, component: LoginV2Component },
        { path: ApplicationPaths.LogOut, component: LogoutComponent },
        { path: ApplicationPaths.LoggedOut, component: LogoutComponent },
        { path: ApplicationPaths.LogOutCallback, component: LogoutComponent }
      ]
    )
  ],
  declarations: [LoginMenuComponent, LogoutComponent, LoginV2Component, RegisterComponent],
  exports: [LoginMenuComponent, LogoutComponent, LoginV2Component]
})
export class ApiAuthorizationModule { }
