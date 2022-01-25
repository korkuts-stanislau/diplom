import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth/auth.service";
import {Auth} from "../../models/auth/auth";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  public authGroup: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.email, Validators.required]),
    password: new FormControl('', [Validators.minLength(5), Validators.required])
  });
  public error = "";

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
  }

  signIn() {
    if(this.authGroup.valid) {
      this.authService.signIn(new Auth(this.authGroup.value.email, this.authGroup.value.password))
        .subscribe(res => {
          
        }, err => {
          this.error = err.error;
        });
    }
  }

  signUp() {
    if(this.authGroup.valid) {
      this.authService.signUp(new Auth(this.authGroup.value.email, this.authGroup.value.password))
        .subscribe(res => {

        }, err => {
          this.error = err.error;
        });
    }
  }
}
