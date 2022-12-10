import {Component} from '@angular/core';
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  constructor(private auth: AuthService) {
  }

  get isLoggedIn() {
    return this.auth.isLoggedIn;
  }

  signIn() {
    this.auth.openSignInDialog();
  }

  signUp() {
    this.auth.openSignUpDialog();
  }

  signOut() {
    this.auth.signOut();
  }

  get fullUserName() {
    return `${this.auth.session?.userInfo.firstName} ${this.auth.session?.userInfo.lastName}`;
  }
}
