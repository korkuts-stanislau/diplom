import { Component, Input, OnInit } from '@angular/core';
import { Card } from 'src/models/resource/card';
import { Project } from 'src/models/resource/project';
import { CardsService } from 'src/services/cards/cards.service';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {

  @Input()public currentProject?:Project;
  public cards?: Card[];

  
  public cardToEdit?:Card;

  constructor(public modalService: ModalService,
              private cardsService: CardsService) { }

  ngOnInit(): void {
    this.cardsService.getCards(this.currentProject!)
      .subscribe(res => {
        this.cards = res;
      }, err => console.log(err));
  }

  createCardModal() {
    this.modalService.openChildModal("create-card");
  }

  createCard(card:Card) {
    this.cardsService.addCard(this.currentProject!, card)
      .subscribe(res => {
        this.cards!.push(res);
      }, err => console.log(err));
  }

  editCardModal(card:Card) {
    this.cardToEdit = {...card};
    this.modalService.openChildModal("edit-card");
  }

  editCard(card:Card) {
    this.cardsService.editCard(card)
      .subscribe(res => {
        let index = this.cards!.findIndex(c => c.id == res.id);
        this.cards![index] = res;
      }, err => console.log(err));
  }

  deleteCard(card:Card) {
    if(confirm("Вы действительно хотите удалить эту карточку?")) {
      this.cardsService.deleteCard(card)
        .subscribe(res => {
          this.cards = this.cards!.filter(c => c.id != card.id);
        }, err => console.log(err));
    }
  }
}
