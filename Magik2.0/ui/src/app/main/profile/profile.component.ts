import { Component, OnDestroy, OnInit } from '@angular/core';
import {ProfilesService} from "../../../services/profile/profiles.service";
import {Profile} from "../../../models/resource/profile";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import { ProfileRoutingService } from 'src/services/routing/profile-routing.service';
import { AuthService } from 'src/services/auth/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  currentProfile?: Profile;
  currentProfileImageUrl?: SafeResourceUrl;

  constructor(private profileService: ProfilesService,
              private sanitizer: DomSanitizer,
              public profileRoutingService: ProfileRoutingService,
              private authService: AuthService) { }

  ngOnDestroy(): void {
    this.profileRoutingService.default();
  }

  ngOnInit(): void {
    this.getProfile();
  }

  getProfile() {
    this.profileService.getProfile()
      .subscribe(res => {
        this.currentProfile = res;
        this.getPictureSource();
      }, err => {
        console.log(err)
      });
  }

  getPictureSource() {
    this.currentProfileImageUrl = this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${this.currentProfile?.picture}`);
  }

  signOut() {
    if(confirm("Вы точно хотите выйти?")) {
      this.authService.signOut();
    }
  }
}
