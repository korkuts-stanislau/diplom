import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { HeaderComponent } from './header/header.component';
import { MainComponent } from './main/main.component';
import { FooterComponent } from './footer/footer.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './main/home/home.component';
import { ProjectsComponent } from './main/projects/projects.component';
import { ProfileComponent } from './main/profile/profile.component';
import { AuthComponent } from './auth/auth.component';
import {AUTH_API_URL, RESOURCE_API_URL, tokenGetter} from "./config/app-injection-tokens";
import {environment} from "../environments/environment";
import {HttpClientModule} from "@angular/common/http";
import {JwtModule} from "@auth0/angular-jwt";
import { ProfileEditComponent } from './main/profile/profile-edit/profile-edit.component';
import { ProfileFriendsComponent } from './main/profile/profile-friends/profile-friends.component';
import { ProfileStatisticComponent } from './main/profile/profile-statistic/profile-statistic.component';
import { ProfileTestsComponent } from './main/profile/profile-tests/profile-tests.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    NavComponent,
    HomeComponent,
    ProjectsComponent,
    ProfileComponent,
    AuthComponent,
    ProfileEditComponent,
    ProfileFriendsComponent,
    ProfileStatisticComponent,
    ProfileTestsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.allowedDomains
      }
    })
  ],
  providers: [
    {
      provide: AUTH_API_URL,
      useValue: environment.authApi
    },
    {
      provide: RESOURCE_API_URL,
      useValue: environment.resourceApi
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
