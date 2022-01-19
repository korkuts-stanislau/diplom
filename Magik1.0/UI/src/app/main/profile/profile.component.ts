import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth/auth.service";
import {ProfileService} from "../../services/profile/profile.service";
import {Profile} from "../../models/profile";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {ReportsService} from "../../services/reports/reports.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile?: Profile;
  photoSource?: SafeResourceUrl;

  isProfileMenu: boolean = false

  photoToUpload: File | null = null;

  constructor(private authService: AuthService,
              private profileService: ProfileService,
              private reportsService: ReportsService,
              private sanitizer: DomSanitizer,
              private cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.profileService.getProfile()
      .subscribe(res => {
        this.profile = res
        this.photoSource = this.sanitizer.bypassSecurityTrustResourceUrl(
          `data:image/png;base64, ${this.profile.photo}`
        );
      }, error => {
        console.log(error)
      });
  }

  handleFileInput(event: Event) {
    let me = this;
    let input = event.target as HTMLInputElement;
    if(input.files) {
      const file = input.files[0];
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
        const result = reader.result!.toString().split(',')[1]; // get only data part from base64
        me.profileService.changeProfilePhoto(result)
          .subscribe(res => {
            me.photoSource = me.sanitizer.bypassSecurityTrustResourceUrl(
              `data:image/png;base64, ${res.base64Photo}`
            );
            me.cdRef.detectChanges();
          }, err => console.log(err));
      };
    }
  }

  changeProfileName() {
    let newProfileName = prompt('Выберите новое имя пользователя');
    if(newProfileName == null) return;
    if(newProfileName.length == 0 || newProfileName.length > 64) {
      alert("Вы ввели неправильные данные (Длина имени должна быть от 1 до 64 символов)");
    }
    else {
      this.profileService.changeProfileName(newProfileName)
        .subscribe(res => {
          this.profile!.userName = newProfileName!;
        }, err => {
          alert(err.error);
        })
    }
  }

  getFullProjectsReport() {
    this.reportsService.getFullProjectsReport()
      .subscribe(res => {
        console.log(res);
        const blob = new Blob([res], { type: 'text/plain;charset=utf-8;' });
        const url = window.URL.createObjectURL(blob);
        window.open(url);
      }, err => {
        console.log(err);
      });
  }

  signout() {
    if (confirm("Вы точно хотите выйти?")) {
      this.authService.signout();
    }
  }
}
