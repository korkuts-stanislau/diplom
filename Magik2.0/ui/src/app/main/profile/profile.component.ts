import { Component, OnInit } from '@angular/core';
import {ProfileService} from "../../../services/profile/profile.service";
import {Profile} from "../../../models/profile/profile";
import {DomSanitizer} from "@angular/platform-browser";
import {ProfileRoutingService} from "../../../services/profile/routing/profile-routing.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  currentProfile?: Profile;

  constructor(private profileService: ProfileService,
              private sanitizer: DomSanitizer,
              public profileRoutingService: ProfileRoutingService) { }

  ngOnInit(): void {
    this.getProfile();
  }

  getProfile() {
    this.profileService.getProfile()
      .subscribe(res => {
        this.currentProfile = res
      }, err => {
        console.log(err)
      });
  }

  getPictureSource() {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${this.currentProfile?.picture}`);
  }
}
