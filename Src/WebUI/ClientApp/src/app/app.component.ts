import { Component, OnInit } from '@angular/core';
import { Authorizev2Service } from 'src/api-authorization/authorizev2.service';

@Component({
  selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private authService: Authorizev2Service) { }

  ngOnInit(): void {
    this.authService.refreshLogin();
  }
}
