import { Component, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../authorize.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.css']
})
export class LoginMenuComponent {
  private authorizeService = inject(AuthorizeService);
  private userService = inject(UserService);

  protected isAuthenticated$: Observable<boolean> = this.authorizeService.getLoggedInState();
  public userName: Observable<string> = this.userService.userName$;
}
