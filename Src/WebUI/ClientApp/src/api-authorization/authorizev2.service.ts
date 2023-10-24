import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable, catchError, map, of } from "rxjs";
import { UserService } from "./user.service";
import { API_BASE_URL } from 'src/app/northwind-traders-api';
import { HttpClient } from "@angular/common/http";


@Injectable ({
    providedIn: 'root'
})
export class Authorizev2Service {

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
            catchError((error) => {
                console.log(error);
                return of(AuthenticationResult.Failure);
            }),
            map((response: NetCore8LoginResponse) => {
                console.log(response);
                this.setAccessToken(response.accessToken);
                this.setRefreshToken(response.refreshToken);
                this.setLoggedInState(true);
                this.userService.setUserName('Dan');
                
                return AuthenticationResult.Success;
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
        if (this.refreshToken === null) {
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
      
        return this.httpClient.post(`${this.baseUrl}/register`, data).pipe(
            catchError((error) => {
                console.log(error);
                return of(AuthenticationResult.Failure);
            }),
            map((response: NetCore8LoginResponse) => {
                console.log(response);
                return AuthenticationResult.Success;
            })
        );
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