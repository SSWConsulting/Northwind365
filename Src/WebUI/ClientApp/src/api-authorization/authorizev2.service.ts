import { Injectable } from "@angular/core";


@Injectable ({
    providedIn: 'root'
})
export class Authorizev2Service {

    accessToken: string;
    refreshToken: string;
    loggedInState: boolean;

    handleLogin(response: NetCore8LoginResponse) {
        this.setAccessToken(response.accessToken);
        this.setRefreshToken(response.refreshToken);
        this.setLoggedInState(true);
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


    getLoggedInState() {
        return this.loggedInState;
    }

    setLoggedInState(loggedInState: boolean) {
        this.loggedInState = loggedInState;
    }



    clearTokens() {
        this.accessToken = null;
        this.refreshToken = null;
    }

    clearLoggedInState() {
        this.loggedInState = false;
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
