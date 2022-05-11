import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Profile } from 'src/models/resource/profile';
import { ModalService } from 'src/services/modal/modal.service';
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
              private profilesService: ProfilesService,
              public modalService: ModalService) { }

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
    if(confirm("Вы хотите удалить этот контакт?")) {
      this.profilesService.deleteContact(contact)
        .subscribe(res => {
          this.acceptedContacts = this.acceptedContacts!.filter(c => c.id != contact.id);
        }, err => console.log(err));
    }
  }

  declineRequest(contact: Profile) {
    if(confirm("Вы хотите отменить запрос в контакты этого пользователя?")) {
      this.profilesService.declineRequest(contact)
        .subscribe(res => {
          this.requestedContacts = this.requestedContacts!.filter(c => c.id != contact.id);
        }, err => console.log(err));
    }
  }

  acceptContact(contact: Profile) {
    if(confirm("Вы хотите добавить этого пользователя в контакты?")) {
      this.profilesService.acceptContact(contact)
        .subscribe(res => {
          this.acceptedContacts!.push(contact);
          this.requestedContacts = this.requestedContacts!.filter(c => c.id != contact.id);
        }, err => console.log(err));
    }
  }

  sendRequestToProfile(contact: Profile) {
    if(confirm("Вы хотите отправить этому пользователю запрос в контакты?")) {
      this.profilesService.sendRequestToProfile(contact)
        .subscribe(res => {
          this.searchedContacts = this.searchedContacts!.filter(c => c.id != contact.id);
        }, err => console.log(err));
    }
  }

  openedProfile?: Profile;
  openProfileModal(contact:Profile) {
    this.profilesService.getOtherProfile(contact)
      .subscribe(res => {
        this.openedProfile = res;
        this.modalService.openModal('show-profile');
      }, err => console.log(err));
  }
}
