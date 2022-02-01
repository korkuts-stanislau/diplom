import { Component, OnInit } from '@angular/core';
import {ProfileService} from "../../../services/profile/profile.service";
import {Profile} from "../../../models/profile/profile";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  currentProfile?: Profile;

  constructor(private profileService: ProfileService) { }

  ngOnInit(): void {
    this.profileService.getProfile()
      .subscribe(res => {
        this.currentProfile = res
      }, err => {
        console.log(err)
      });
  }

}
