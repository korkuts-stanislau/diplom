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

  routeToFriends() {
    this.currentWindow = "friends";
  }

  routeToStatistic() {
    this.currentWindow = "statistic";
  }

  routeToTests() {
    this.currentWindow = "tests";
  }

  isEdit():boolean {
    return this.currentWindow == "edit";
  }

  isFriends():boolean {
    return this.currentWindow == "friends";
  }

  isStatistic():boolean {
    return this.currentWindow == "statistic";
  }

  isTests():boolean {
    return this.currentWindow == "tests";
  }
}
