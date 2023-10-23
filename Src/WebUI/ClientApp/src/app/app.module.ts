import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app.routing.module';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ProductsComponent } from './products/products.component';
import { CustomersComponent } from './customers/customers.component';
import { NavTopMenuComponent } from './nav-top-menu/nav-top-menu.component';
import { NavSideMenuComponent } from './nav-side-menu/nav-side-menu.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';

import { Client, API_BASE_URL } from './northwind-traders-api';

import { CamelCaseToText } from '../pipes/camel-case-to-text';

import { ModalModule } from 'ngx-bootstrap/modal';
import { AppIconsModule } from './app.icons.module';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { environment } from "../environments/environment";
import { OidcApiConfiguration } from 'src/api-authorization/authorize.service';
import { AuthModule } from 'angular-auth-oidc-client';
import { ApplicationName } from 'src/api-authorization/api-authorization.constants';

const hardcodedOidcConfig: OidcApiConfiguration = {
  "authority": "https://localhost:44427",
  "client_id": "Northwind.WebUI",
  "redirect_uri": "https://localhost:44427/authentication/login-callback",
  "post_logout_redirect_uri": "https://localhost:44427/authentication/logout-callback",
  "response_type": "code",
  "scope": "Northwind.WebUIAPI openid profile"
}

@NgModule({
  declarations: [
    AppComponent,
    NavTopMenuComponent,
    NavSideMenuComponent,
    HomeComponent,
    ProductsComponent,
    CustomersComponent,
    CustomerDetailComponent,
    CamelCaseToText
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppIconsModule,
    AppRoutingModule,
    ApiAuthorizationModule,
    AuthModule.forRoot({
      config: {
        authority: hardcodedOidcConfig.authority,
        // TODO: move this elsewhere?
        redirectUrl: hardcodedOidcConfig.redirect_uri,
        postLogoutRedirectUri: hardcodedOidcConfig.post_logout_redirect_uri,
        clientId: ApplicationName,
        scope: hardcodedOidcConfig.scope,
        responseType: hardcodedOidcConfig.response_type,
        silentRenew: true,
        useRefreshToken: true,
        // logLevel: LogLevel.Debug,
      },
    }),
    ModalModule.forRoot()
  ],
  providers: [
      { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
      { provide: API_BASE_URL, useValue: environment.apiBaseUrl },
      Client,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
