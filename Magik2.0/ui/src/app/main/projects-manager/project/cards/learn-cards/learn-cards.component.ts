import { Component, Input, OnInit } from '@angular/core';
import { Card } from 'src/models/resource/card';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-learn-cards',
  templateUrl: './learn-cards.component.html',
  styleUrls: ['./learn-cards.component.css']
})
export class LearnCardsComponent implements OnInit {

  @Input()cards?:Card[];
  public currentCard?:Card;
  public isAnswerShown = false;

  constructor(public modalService:ModalService) { }

  ngOnInit(): void {
    this.chooseRandomCard();
  }

  chooseRandomCard() {
    this.isAnswerShown = false;
    if(this.cards?.length == 1) {
      this.currentCard = this.cards[0];
      return;
    }
    let newCard = this.cards![Math.floor(Math.random() * this.cards!.length)];
    while(newCard == this.currentCard) newCard = this.cards![Math.floor(Math.random() * this.cards!.length)];
    this.currentCard = newCard;
  }
}
