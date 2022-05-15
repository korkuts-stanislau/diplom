import { Component, OnInit } from '@angular/core';
import { Group } from 'src/models/chat/group';
import { Profile } from 'src/models/resource/profile';
import { ProfilesService } from 'src/services/profile/profiles.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public currentProfile?:Profile;
  public currentGroup?:Group;

  constructor(private profilesService:ProfilesService) { }

  ngOnInit(): void {
    this.getCurrentProfile();
  }

  getCurrentProfile() {
    this.profilesService.getProfile()
      .subscribe(res => {
        this.currentProfile = res;
      }, err => console.log(err));
  }

  selectGroup(group:Group) {
    this.currentGroup = group;
  }
}
