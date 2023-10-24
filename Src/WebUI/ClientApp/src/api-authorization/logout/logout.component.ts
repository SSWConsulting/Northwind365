import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { LogoutActions, ApplicationPaths, ReturnUrlType } from '../api-authorization.constants';
import { AuthorizeService } from '../authorize.service';

// The main responsibility of this component is to handle the user's logout process.
// This is the starting point for the logout process, which is usually initiated when a
// user clicks on the logout button on the LoginMenu component.
@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {
  public message = new BehaviorSubject<string>(null);

  constructor(
    private authorizeService: AuthorizeService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  async ngOnInit() {
    const action = this.activatedRoute.snapshot.url[1];
    switch (action.path) {
      case LogoutActions.Logout:
        if (!!window.history.state.local) {
          await this.logout();
        } else {
          // This prevents regular links to <app>/authentication/logout from triggering a logout
          this.message.next('The logout was not initiated from within the page.');
        }

        break;
      case LogoutActions.LogoutCallback:
        await this.processLogoutCallback();
        break;
      case LogoutActions.LoggedOut:
        this.message.next('You successfully logged out!');
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  }

  private async logout(): Promise<void> {
    const isauthenticated = await this.authorizeService.getLoggedInState().pipe(
      take(1)
    ).toPromise();
    if (isauthenticated) {
      await this.navigateToReturnUrl("/");
    } else {
      this.message.next('You successfully logged out!');
    }
  }

  private async processLogoutCallback(): Promise<void> {
    const url = window.location.href;
    this.authorizeService.logout();
    await this.navigateToReturnUrl("/");
  }

  private async navigateToReturnUrl(returnUrl: string) {
    await this.router.navigateByUrl(returnUrl, {
      replaceUrl: true
    });
  }
}
