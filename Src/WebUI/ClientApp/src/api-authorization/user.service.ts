import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";


@Injectable({
    providedIn: 'root'
})
export class UserService {
    isLoggedIn = false;
    redirectUrl: string;
    constructor() { }

    userNameSubject: BehaviorSubject<string> = new BehaviorSubject<string>('');


    getUserName(): Observable<string> {
        return this.userNameSubject.asObservable();
    }

    setUserName (userName: string) {
        this.userNameSubject.next(userName);
    }
}
