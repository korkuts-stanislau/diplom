import { Component, OnInit } from '@angular/core';
import {AuthService} from "../services/auth/auth.service";
import {SignIn} from "../models/auth/signIn";
import {SignUp} from "../models/auth/signUp";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  emailText: string = "";
  passwordText: string = "";
  confirmationPasswordText: string = "";
  rememberMe: boolean = false;
  error: string = "";
  message: string = "";
  action: string = "Авторизация" //Can be Авторизация или Регистрация

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  validateFields(): boolean {
    if(this.emailText == "" || this.passwordText == "") {
      this.error = "Вы ввели не все данные";
      return false;
    }
    if(this.action == "Регистрация") {
      if(this.passwordText != this.confirmationPasswordText) {
        this.error = "Пароли должны совпадать";
        return false;
      }
    }
    return true;
  }

  signin() {
    if(this.validateFields()) {
      this.authService.signin(new SignIn(this.emailText,
        this.passwordText,
        this.rememberMe))
        .subscribe(res => {

        }, error => {
          console.log(error)
          if(error.status == 0) {
            this.error = "Сервер не доступен"
          }
          else {
            this.error = error["error"];
          }
        });
    }
  }

  signup() {
    if(this.validateFields()) {
      this.authService.signup(new SignUp(this.emailText,
        this.passwordText))
        .subscribe(res => {
              this.showAuthorization();
              this.message = "Вы были зарегистрированы";
          },
          error => {
            if(error.status == 0) {
              this.error = "Сервер не доступен"
            }
            else {
              this.error = error["error"];
            }
          });
    }
  }


  showRegistration() {
    this.action = "Регистрация"
    this.error = ""
    this.message = ""
  }

  showAuthorization() {
    this.action = "Авторизация"
    this.error = ""
    this.message = ""
  }
}
