import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import {HttpClientModule} from "@angular/common/http";
import { MainComponent } from './main/main.component';
import {API_URL} from "./app-injection-tokens";
import {environment} from "../environments/environment";
import {JwtModule} from "@auth0/angular-jwt";
import {ACCESS_TOKEN_KEY} from "./services/auth/auth.service";
import {FormsModule} from "@angular/forms";
import { ProfileComponent } from './main/profile/profile.component';
import { ProjectAreasComponent } from './main/project-areas/project-areas.component';
import { ProjectsComponent } from './main/projects/projects.component';
import { ProjectComponent } from './main/projects/project/project.component';
import { ProjectPartsComponent } from './main/projects/project/project-parts/project-parts.component';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    MainComponent,
    ProfileComponent,
    ProjectAreasComponent,
    ProjectsComponent,
    ProjectComponent,
    ProjectPartsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.allowedDomains,
        headerName: "Authorization"
      }
    })
  ],
  providers: [{
    provide: API_URL,
    useValue: environment.api
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
