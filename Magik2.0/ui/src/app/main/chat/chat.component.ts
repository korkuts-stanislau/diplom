import { Component, Inject, OnInit } from '@angular/core';
import { Group } from 'src/models/chat/group';
import * as signalR from "@microsoft/signalr";
import { Profile } from 'src/models/resource/profile';
import { ProfilesService } from 'src/services/profile/profiles.service';
import { CHAT_API_URL } from 'src/app/config/app-injection-tokens';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public currentProfile?:Profile;
  public currentGroup?:Group;
  public hub?: signalR.HubConnection;

  constructor(private profilesService:ProfilesService,
              @Inject(CHAT_API_URL)private url:string) { }

  ngOnInit(): void {
    this.hub = new signalR.HubConnectionBuilder()
                            .withUrl(`${this.url}chat`)
                            .build();

    this.hub.start()
      .then(() => this.hub?.invoke("SendGroups"))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.getCurrentProfile();
  }

  getCurrentProfile() {
    this.profilesService.getProfile()
      .subscribe(res => {
        this.currentProfile = res;
      }, err => console.log(err));
  }
}
