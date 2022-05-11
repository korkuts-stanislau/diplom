import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Card } from 'src/models/resource/card';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-create-card',
  templateUrl: './create-card.component.html',
  styleUrls: ['./create-card.component.css']
})
export class CreateCardComponent implements OnInit {

  public newCard: Card = new Card("", "", 0);
  @Output() cardCreated: EventEmitter<Card> = new EventEmitter<Card>();

  constructor(public modalService:ModalService) { }

  ngOnInit(): void {
  }

  createCard() {
    this.cardCreated.emit(this.newCard);
    this.modalService.closeChild();
  }
}
