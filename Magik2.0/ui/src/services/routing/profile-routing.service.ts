import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProfileRoutingService {
  private currentWindow:string = "statistic";

  constructor() {}

  default() {
    this.currentWindow = "statistic";
  }

  routeToEdit() {
    this.currentWindow = "edit";
  }

  routeToContacts() {
    this.currentWindow = "contacts";
  }

  routeToStatistic() {
    this.currentWindow = "statistic";
  }

  isEdit():boolean {
    return this.currentWindow == "edit";
  }

  isContacts():boolean {
    return this.currentWindow == "contacts";
  }

  isStatistic():boolean {
    return this.currentWindow == "statistic";
  }
}
