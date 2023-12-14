import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userNameSubject$ = new BehaviorSubject<string>('');
  public readonly userName$ = this.userNameSubject$.asObservable();

  setUserName (userName: string) {
    this.userNameSubject$.next(userName);
  }
}
