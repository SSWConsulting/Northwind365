import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app.routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ProductsComponent } from './products/products.component';
import { CustomersComponent } from './customers/customers.component';
import { NavTopMenuComponent } from './nav-top-menu/nav-top-menu.component';
import { NavSideMenuComponent } from './nav-side-menu/nav-side-menu.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';

import { API_BASE_URL, Client } from './northwind-traders-api';

import { CamelCaseToText } from '../pipes/camel-case-to-text';

import { ModalModule } from 'ngx-bootstrap/modal';
import { AppIconsModule } from './app.icons.module';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { environment } from "../environments/environment";
import { ToastrModule } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

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
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppIconsModule,
    AppRoutingModule,
    ApiAuthorizationModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot(),
  ],
  providers: [
      { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
      { provide: API_BASE_URL, useValue: environment.apiBaseUrl },
      Client,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
