import {Component, Input, OnInit} from '@angular/core';
import { ProfileRoutingService } from 'src/services/routing/profile-routing.service';
import {Profile} from "../../../../models/resource/profile";
import {ProfilesService} from "../../../../services/profile/profiles.service";
import { ProfileComponent } from '../profile.component';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit {
  @Input()public currentProfile?: Profile;
  @Input()public parent?: ProfileComponent;
  private newPicture: string = "";

  public error = "";
  public info = "";

  public isPictureEdited: boolean = false;

  constructor(private profileService: ProfilesService) { }
  
  ngOnInit(): void {
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
        me.newPicture = reader.result!.toString().split(',')[1];
        me.isPictureEdited = true;
      };
    }
  }

  editProfile() {
    let message: string | undefined = this.profileService.validateProfile(this.currentProfile)
    if(message) {
      this.error = message;
      this.info = "";
    }
    else {
      let newProfile = new Profile(
        this.currentProfile!.id,
        this.currentProfile!.username,
        this.currentProfile!.description,
        undefined,
        this.newPicture);
      this.profileService.editProfile(newProfile, this.isPictureEdited)
      .subscribe(res => {
        this.info = "Профиль успешно изменён";
        this.error = "";
        if(this.isPictureEdited) this.currentProfile!.picture = this.newPicture;
        this.parent?.getPictureSource();
      }, err => {
        this.error = err.message;
        this.info = "";
      });
      this.isPictureEdited = false;
    }
  }
}
