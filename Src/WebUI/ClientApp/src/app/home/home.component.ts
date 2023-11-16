import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

declare var bootstrap: any;

@Component({
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit
{
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params["loggedOut"] === "true") {
        this.showLoggedOutToast();
      }
    });
  }

  showLoggedOutToast() {
    const toastRef = document.getElementById("logoutToast");
    const toast = bootstrap.Toast.getOrCreateInstance(toastRef);
    toast.show();
  }

}
