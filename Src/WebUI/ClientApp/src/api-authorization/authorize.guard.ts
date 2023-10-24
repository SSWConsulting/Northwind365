import { Injectable, inject } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { ApplicationPaths, QueryParameterNames } from './api-authorization.constants';
import { Authorizev2Service } from './authorizev2.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {

  constructor(private router: Router, private authService: Authorizev2Service) {
  }
  canActivate(
    _next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> {
      return this.authService.getLoggedInState().pipe(
        map(isAuthenticated => this.handleAuthorization(isAuthenticated, state))
      );
  }

  private handleAuthorization(isAuthenticated: boolean, state: RouterStateSnapshot): UrlTree | boolean {
    if (isAuthenticated) {
      return true;
    }

    return this.router.createUrlTree(ApplicationPaths.LoginPathComponents, {
      queryParams: {
        [QueryParameterNames.ReturnUrl]: state.url
      }
    });
  }
}
