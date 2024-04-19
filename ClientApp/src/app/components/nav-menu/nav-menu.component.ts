import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "src/app/services/authentication.service";
import { User } from "src/app/models/user";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html"
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser: User;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(
      x => (this.currentUser = x)
    );
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(["/login"]);
  }

  changePassword() {
    this.router.navigate(["/password"]);
  }

  collapse() {
    this.isExpanded = false;
  }

  registerUser() {
    this.router.navigate(["/register"]);
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
