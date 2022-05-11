import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { HeaderComponent } from './header/header.component';
import { MainComponent } from './main/main.component';
import { FooterComponent } from './footer/footer.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './main/home/home.component';
import { ProfileComponent } from './main/profile/profile.component';
import { AuthComponent } from './auth/auth.component';
import {AUTH_API_URL, CHAT_API_URL, RESOURCE_API_URL, tokenGetter} from "./config/app-injection-tokens";
import {environment} from "../environments/environment";
import {HttpClientModule} from "@angular/common/http";
import {JwtModule} from "@auth0/angular-jwt";
import { ProfileEditComponent } from './main/profile/profile-edit/profile-edit.component';
import { ProfileStatisticComponent } from './main/profile/profile-statistic/profile-statistic.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FieldsComponent } from './main/projects-manager/fields/fields.component';
import { AddFieldComponent } from './main/projects-manager/fields/add-field/add-field.component';
import { EditFieldComponent } from './main/projects-manager/fields/edit-field/edit-field.component';
import { ProjectsManagerComponent } from './main/projects-manager/projects-manager.component';
import { ProjectComponent } from './main/projects-manager/project/project.component';
import { StagesComponent } from './main/projects-manager/stages/stages.component';
import { EditProjectComponent } from './main/projects-manager/project/edit-project/edit-project.component';
import { AddStageComponent } from './main/projects-manager/project/add-stage/add-stage.component';
import { EditStageComponent } from './main/projects-manager/stages/edit-stage/edit-stage.component';
import { AttachmentsComponent } from './main/projects-manager/attachments/attachments.component';
import { EditAttachmentComponent } from './main/projects-manager/attachments/edit-attachment/edit-attachment.component';
import { CreateAttachmentComponent } from './main/projects-manager/attachments/create-attachment/create-attachment.component';
import { ChatComponent } from './main/chat/chat.component';
import { ProfileContactsComponent } from './main/profile/profile-contacts/profile-contacts.component';
import { ProfileShowComponent } from './main/profile/profile-contacts/profile-show/profile-show.component';
import { CardsComponent } from './main/projects-manager/project/cards/cards.component';
import { CreateCardComponent } from './main/projects-manager/project/cards/create-card/create-card.component';
import { EditCardComponent } from './main/projects-manager/project/cards/edit-card/edit-card.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    MainComponent,
    FooterComponent,
    NavComponent,
    HomeComponent,
    ProjectsManagerComponent,
    ProfileComponent,
    AuthComponent,
    ProfileEditComponent,
    ProfileContactsComponent,
    ProfileStatisticComponent,
    FieldsComponent,
    AddFieldComponent,
    EditFieldComponent,
    ProjectComponent,
    StagesComponent,
    EditProjectComponent,
    AddStageComponent,
    EditStageComponent,
    AttachmentsComponent,
    EditAttachmentComponent,
    CreateAttachmentComponent,
    ChatComponent,
    ProfileShowComponent,
    CardsComponent,
    CreateCardComponent,
    EditCardComponent
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
    }),
    BrowserAnimationsModule
  ],
  providers: [
    {
      provide: AUTH_API_URL,
      useValue: environment.authApi
    },
    {
      provide: RESOURCE_API_URL,
      useValue: environment.resourceApi
    },
    {
      provide: CHAT_API_URL,
      useValue: environment.chatApi
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
