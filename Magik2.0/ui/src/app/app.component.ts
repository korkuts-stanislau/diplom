import { Component } from '@angular/core';
import {AuthService} from "../services/auth/auth.service";
import {AppRoutingService} from "../services/routing/routing.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(public auth: AuthService,
              public routing: AppRoutingService) {
  }


}
