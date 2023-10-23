import { Injectable, inject } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from './authorize.service';
import { map, tap } from 'rxjs/operators';
import { ApplicationPaths, QueryParameterNames } from './api-authorization.constants';
import { AuthenticatedResult, OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {
  private oidcSecurityService = inject(OidcSecurityService);

  constructor(private router: Router) {
  }
  canActivate(
    _next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> {
      return this.oidcSecurityService.isAuthenticated$.pipe(
        map(isAuthenticated => this.handleAuthorization(isAuthenticated, state))
      );
  }

  private handleAuthorization(isAuthenticated: AuthenticatedResult, state: RouterStateSnapshot): UrlTree | boolean {
    if (isAuthenticated.isAuthenticated) {
      return true;
    }

    return this.router.createUrlTree(ApplicationPaths.LoginPathComponents, {
      queryParams: {
        [QueryParameterNames.ReturnUrl]: state.url
      }
    });
  }
}
