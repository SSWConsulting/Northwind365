import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserService } from "./user.service";


@Injectable ({
    providedIn: 'root'
})
export class Authorizev2Service {

    constructor(private userService: UserService) { }

    accessToken: string;
    refreshToken: string;
    loggedInStateSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    handleLogin(response: NetCore8LoginResponse) {
        this.setAccessToken(response.accessToken);
        this.setRefreshToken(response.refreshToken);
        this.setLoggedInState(true);
        this.userService.setUserName('Dan');
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

}

export interface NetCore8LoginResponse {
    tokenType: string;
    accessToken: string;
    expiresIn: number;
    refreshToken: string;
  }
