import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { ApplicationPaths, QueryParameterNames } from './api-authorization.constants';
import { AuthorizeService } from './authorize.service';

export const AuthorizeGuardFn: CanActivateFn = (_, state) => {
  const router = inject(Router);
  const authService = inject(AuthorizeService);

  return authService.getLoggedInState().pipe(
    map(isAuthenticated => {
      if (isAuthenticated) {
        return true;
      }

      return router.createUrlTree(ApplicationPaths.LoginPathComponents, {
        queryParams: {
          [QueryParameterNames.ReturnUrl]: state.url
        }
      });
    })
  );
}
