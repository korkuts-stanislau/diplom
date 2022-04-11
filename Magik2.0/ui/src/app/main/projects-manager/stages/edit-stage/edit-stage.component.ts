import { Component, Input, OnInit } from '@angular/core';
import { Stage } from 'src/models/resource/stage';
import { ModalService } from 'src/services/modal/modal.service';
import { StagesComponent } from '../stages.component';

@Component({
  selector: 'app-edit-stage',
  templateUrl: './edit-stage.component.html',
  styleUrls: ['./edit-stage.component.css']
})
export class EditStageComponent implements OnInit {
  
  @Input()public parent?:StagesComponent;
  @Input()public stageToEdit?:Stage;

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  editStage() {
    this.parent!.editStage(this.stageToEdit!);
    this.modalService.closeModal();
  }

  currentDate(): Date {
    return new Date();
  }

  defaultDeadline() {
    // tomorow
    let result = this.currentDate();
    result.setDate(result.getDate() + 1);
    return result;
  }
}
