import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Profile} from "../../../../models/profile/profile";
import {ProfileService} from "../../../../services/profile/profile.service";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit, OnDestroy {
  @Input()public currentProfile?: Profile;

  constructor(private profileService: ProfileService) { }
  
  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.editProfile();
  }

  editPhotoEvent(event: Event) {
    let me = this;
    let input = event.target as HTMLInputElement;
    if(input.files) {
      const file = input.files[0];
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
         // get only data part from base64
        me.currentProfile!.picture = reader.result!.toString().split(',')[1];
      };
    }
  }

  editProfile() {
    let message: string | undefined = this.profileService.validateProfile(this.currentProfile)
    if(message) {
      alert(message);
    }
    else {
      this.profileService.editProfile(this.currentProfile!)
      .subscribe(res => {

      }, err => {
        console.log(err);
      });
    }
  }
}
