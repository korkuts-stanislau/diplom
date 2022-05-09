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

  isContactsShown = true;
  isSearchedProfilesShown = false;
  acceptedContacts?:Profile[];
  requestedContacts?:Profile[];
  searchedContacts?:Profile[];

  searchField:string = "";

  constructor(private sanitizer: DomSanitizer,
              private profilesService: ProfilesService) { }

  ngOnInit(): void {
    this.getContacts();
    this.getRequests();
  }

  showContacts() {
    this.isContactsShown = true;
    this.isSearchedProfilesShown = false;
  }

  showSearchResults() {
    this.isContactsShown = false;
    this.isSearchedProfilesShown = true;
  }

  getContactIconSource(contact:Profile) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${contact.icon}`);
  }

  getContacts() {
    this.profilesService.getAcceptedContacts()
      .subscribe(res => {
        this.acceptedContacts = res;
        this.showContacts();
      }, err => console.log(err));
  }

  getRequests() {
    this.profilesService.getRequestedContacts()
      .subscribe(res => {
        this.requestedContacts = res;
        this.showContacts();
      }, err => console.log(err));
  }

  searchContactsByName() {
    this.profilesService.searchProfilesByName(this.searchField)
      .subscribe(res => {
        this.searchedContacts = res;
        this.showSearchResults();
      }, err => console.log(err));
  }

  searchContactsByDescription() {
    this.profilesService.searchProfilesByDescription(this.searchField)
      .subscribe(res => {
        this.searchedContacts = res;
        this.showSearchResults();
      }, err => console.log(err));
  }

  deleteContact(contact: Profile) {
    if(confirm("Вы действительно хотите удалить этот контакт?")) {
      
    }
  }

  acceptContact(contact: Profile) {

  }

  sendRequestToProfile(contact: Profile) {
    
  }

  openProfileModal(contact:Profile) {

  }
}
