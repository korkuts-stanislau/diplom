import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Profile } from 'src/models/resource/profile';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-profile-show',
  templateUrl: './profile-show.component.html',
  styleUrls: ['./profile-show.component.css']
})
export class ProfileShowComponent implements OnInit {

  @Input() public profile?: Profile;

  constructor(public modalService: ModalService,
              private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
  }

  getPictureSource() {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${this.profile?.picture}`);
  }
}
