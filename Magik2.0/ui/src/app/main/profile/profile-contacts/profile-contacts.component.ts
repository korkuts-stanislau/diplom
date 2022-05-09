import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Profile } from 'src/models/resource/profile';
import { ProfilesService } from 'src/services/profile/profiles.service';

@Component({
  selector: 'app-profile-contacts',
  templateUrl: './profile-contacts.component.html',
  styleUrls: ['./profile-contacts.component.css']
})
export class ProfileContactsComponent implements OnInit {

  isMyContactsShown = true;
  isSearchedProfilesShown = false;
  acceptedContacts?:Profile[];
  requestedContacts?:Profile[];

  constructor(private sanitizer: DomSanitizer,
              private profilesService: ProfilesService) { }

  ngOnInit(): void {
    this.getContacts();
    this.getRequests();
  }

  getContactIconSource(contact:Profile) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${contact.icon}`);
  }

  getContacts() {
    this.profilesService.getAcceptedContacts()
      .subscribe(res => {
        this.acceptedContacts = res;
      }, err => console.log(err));
  }

  getRequests() {
    this.profilesService.getRequestedContacts()
      .subscribe(res => {
        this.requestedContacts = res;
      }, err => console.log(err));
  }

  deleteContact(contact: Profile) {
    if(confirm("Вы действительно хотите удалить этот контакт?")) {
      
    }
  }
}
