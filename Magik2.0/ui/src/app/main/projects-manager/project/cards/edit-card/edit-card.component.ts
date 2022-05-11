import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Card } from 'src/models/resource/card';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-edit-card',
  templateUrl: './edit-card.component.html',
  styleUrls: ['./edit-card.component.css']
})
export class EditCardComponent implements OnInit {

  @Input()cardToEdit?:Card;
  @Output()cardEdited: EventEmitter<Card> = new EventEmitter<Card>();

  constructor(public modalService:ModalService) { }

  ngOnInit(): void {
  }

  editCard() {
    this.cardEdited.emit(this.cardToEdit);
    this.modalService.closeChild();
  }
}
