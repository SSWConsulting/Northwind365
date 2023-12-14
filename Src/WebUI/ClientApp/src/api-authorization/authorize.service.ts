import { inject, Injectable } from "@angular/core";
import { AsyncSubject, BehaviorSubject, catchError, map, Observable, of } from "rxjs";
import { UserService } from "./user.service";
import { Client, LoginRequest, RegisterRequest } from 'src/app/northwind-traders-api';

@Injectable ({
  providedIn: 'root'
})
export class AuthorizeService {
  private userService = inject(UserService);
  private authClient = inject(Client);

  private accessToken: string;
  private refreshToken: string;
  private loggedInStateSubject$ = new BehaviorSubject<boolean>(false);

  handleLogin(loginModel: LoginRequest): Observable<AuthenticationResult> {
    const data: LoginRequest = {
      email: loginModel.email,
      password: loginModel.password
    };

    return this.authClient.postApiAuthLogin(undefined, undefined, data).pipe(
      map(response => {
        if (!response.accessToken || !response.refreshToken) {
          throw new Error('Invalid login response');
        }

        this.setAccessToken(response.accessToken);
        this.setRefreshToken(response.refreshToken);
        this.setLoggedInState(true);
        this.userService.setUserName(data.email);

        return AuthenticationResult.Success;
      }),
      catchError(error => {
        console.error(error);
        return of(AuthenticationResult.Failure);
      })
    );
  }

  getAccessToken() {
    return this.accessToken;
  }

  private setAccessToken(accessToken: string) {
    this.accessToken = accessToken;
  }

  private getRefreshToken() {
    if (!this.refreshToken) {
      this.refreshToken = sessionStorage.getItem('refreshToken');
    }
    return this.refreshToken;
  }

  private setRefreshToken(refreshToken: string) {
    this.refreshToken = refreshToken;
    sessionStorage.setItem('refreshToken', refreshToken);
  }


  getLoggedInState(): Observable<boolean | null> {
    return this.loggedInStateSubject$.asObservable();
  }

  private setLoggedInState(loggedInState: boolean) {
    this.loggedInStateSubject$.next(loggedInState);
  }


  private clearTokens() {
    this.accessToken = null;
    this.refreshToken = null;
  }

  private clearLoggedInState() {
    this.loggedInStateSubject$.next(false);
    this.userService.setUserName(null);
  }


  logout () {
    this.clearTokens();
    this.clearLoggedInState();
    sessionStorage.clear();
  }

  registerUser(registerModel: RegisterRequest): Observable<AuthenticationResult> {
    const data = {
      email: registerModel.email,
      password: registerModel.password
    };

    return this.authClient.postApiAuthRegister(data).pipe(
      map(() => AuthenticationResult.Success),
      catchError((error) => {
        console.error(error);
        return of(AuthenticationResult.Failure);
      })
    );
  }

  refreshLogin(): Observable<AuthenticationResult> {
    const authenticationResult$ = new AsyncSubject<AuthenticationResult>();
    this.getRefreshToken();

    if (!this.refreshToken) {
      console.error("No refresh token found");
      return of(AuthenticationResult.Failure);
    }

    this.authClient.postApiAuthRefresh({
      refreshToken: this.refreshToken
    }).pipe(
      map(response => {
        if (response.accessToken && response.refreshToken) {
          this.setAccessToken(response.accessToken);
          this.setRefreshToken(response.refreshToken);
          this.setLoggedInState(true);
          this.setUserName();
          return AuthenticationResult.Success;
        }
        else {
          return AuthenticationResult.Failure;
        }
      })
    ).subscribe(result => {
      authenticationResult$.next(result);
      authenticationResult$.complete();
    });

    return authenticationResult$.asObservable();
  }

  setUserName() {
    this.authClient.getApiAuthManageInfo().subscribe(result => {
      this.userService.setUserName(result.email);
    })
  }
}

export enum AuthenticationResult {
  Success,
  Failure
}
