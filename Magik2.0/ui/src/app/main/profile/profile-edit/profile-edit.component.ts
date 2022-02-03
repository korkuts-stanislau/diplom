import {Component, Input, OnInit} from '@angular/core';
import {Profile} from "../../../../models/profile/profile";
import {ProfileService} from "../../../../services/profile/profile.service";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit {
  @Input()public currentProfile?: Profile;

  constructor(private profileService: ProfileService) { }

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
        me.currentProfile!.picture = reader.result!.toString().split(',')[1];
      };
    }
  }

  editProfile() {
    this.profileService.editProfile(this.currentProfile!)
      .subscribe(res => {
        alert("Профиль успешно изменён");
      }, err => {
        alert(err.message);
      })
  }
}
