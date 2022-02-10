import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  constructor() { }

  private currentModalName: string = "";

  public openModal(modalName:string) {
    this.currentModalName = modalName;
  }

  public isModalOpened(modalName:string) {
    return this.currentModalName == modalName;
  }

  public closeModal() {
    this.currentModalName = "";
  }
}
