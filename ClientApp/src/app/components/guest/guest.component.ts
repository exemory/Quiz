import {Component} from '@angular/core';
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-guest',
  templateUrl: './guest.component.html',
  styleUrls: ['./guest.component.scss']
})
export class GuestComponent {

  constructor(private auth: AuthService) {
  }

  signIn() {
    this.auth.openSignInDialog();
  }

  signUp() {
    this.auth.openSignUpDialog();
  }
}
