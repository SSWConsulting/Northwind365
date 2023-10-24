import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable, catchError, map, of, tap } from "rxjs";
import { UserService } from "./user.service";
import { API_BASE_URL } from 'src/app/northwind-traders-api';
import { HttpClient } from "@angular/common/http";


@Injectable ({
    providedIn: 'root'
})
export class AuthorizeService {

    constructor(
        private userService: UserService,
        private httpClient: HttpClient,
        @Inject(API_BASE_URL) private baseUrl?: string) { }

    accessToken: string;
    refreshToken: string;
    loggedInStateSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    handleLogin(loginModel: NetCore8LoginModel): Observable<AuthenticationResult> {
        let data = {
          email: loginModel.email,
          password: loginModel.password
        };
        
        return this.httpClient.post<NetCore8LoginResponse>(`${this.baseUrl}/login`, data).pipe(
          tap(response => {
            if (!response.accessToken || !response.refreshToken) {
              throw new Error('Invalid login response');
            }
          }),
          map(response => {
            this.setAccessToken(response.accessToken);
            this.setRefreshToken(response.refreshToken);
            this.setLoggedInState(true);
            this.userService.setUserName(data.email);
            
            return AuthenticationResult.Success;
          }),
          catchError(error => {
            console.log(error);
            return of(AuthenticationResult.Failure);
          })
        );
      }

    getAccessToken() {
        return this.accessToken;
    }

    setAccessToken(accessToken: string) {
        this.accessToken = accessToken;
    }


    getRefreshToken() {
        if (!this.refreshToken) {
            this.refreshToken = sessionStorage.getItem('refreshToken');
        }
        return this.refreshToken;
    }

    setRefreshToken(refreshToken: string) {
        this.refreshToken = refreshToken;
        sessionStorage.setItem('refreshToken', refreshToken);
    }


    getLoggedInState(): Observable<boolean | null> {
        return this.loggedInStateSubject.asObservable();
    }

    setLoggedInState(loggedInState: boolean) {
        this.loggedInStateSubject.next(loggedInState);
    }


    clearTokens() {
        this.accessToken = null;
        this.refreshToken = null;
    }

    clearLoggedInState() {
        this.loggedInStateSubject.next(false);
    }


    logout () {
        this.clearTokens();
        this.clearLoggedInState();
        sessionStorage.clear();
    }

    registerUser(registerModel: NetCore8RegisterModel): Observable<AuthenticationResult> {
        
        let data = {
            email: registerModel.email,
            password: registerModel.password
        };
      
        return this.httpClient.post(`${this.baseUrl}/register`, data, {observe: 'response'}).pipe(
            map((response) => {
                if (response.status == 200) {
                    return AuthenticationResult.Success;
                } else {
                    return AuthenticationResult.Failure;
                }
            }),
            catchError((error) => {
                console.log(error);
                return of(AuthenticationResult.Failure);
            })
        );
    }

    refreshLogin(): Observable<AuthenticationResult> {

        this.getRefreshToken();

        if (!this.refreshToken) {
            console.log("No refresh token found");
            return of(AuthenticationResult.Failure);
        }

        console.log("Refreshing login")

        let data = {
            refreshToken: this.refreshToken
        };

        return this.httpClient.post<NetCore8LoginResponse>(`${this.baseUrl}/refresh`, data).pipe(
            map(response => {
            if (response.accessToken && response.refreshToken) {
                this.setAccessToken(response.accessToken);
                this.setRefreshToken(response.refreshToken);
                this.setLoggedInState(true);
                this.userService.setUserName('Dan');
                return AuthenticationResult.Success;
            }
            else {
                return AuthenticationResult.Failure;
            }
        }));
    }

}

export interface NetCore8LoginResponse {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;
}

export interface NetCore8LoginModel {
    email: string;
    password: string;
}

export interface NetCore8RegisterModel {
    email: string;
    password: string;
}

export enum AuthenticationResult {
    Success,
    Failure
}

export interface RefreshModel {
    refreshToken: string;
}