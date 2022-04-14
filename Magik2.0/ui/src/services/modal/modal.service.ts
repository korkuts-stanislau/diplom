import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  constructor() { }

  private currentModalName: string = "";
  private modalChild: string = "";
 
  public openModal(modalName:string) {
    this.currentModalName = modalName;
  }

  public isModalOpened(modalName:string) {
    return this.currentModalName == modalName;
  }

  public closeModal() {
    this.currentModalName = "";
  }

  public openChildModal(childName:string) {
    this.modalChild = childName;
  }

  public isChildOpened(childName:string) {
    return this.modalChild == childName;
  }

  public closeChild() {
    this.modalChild = "";
  }
}
