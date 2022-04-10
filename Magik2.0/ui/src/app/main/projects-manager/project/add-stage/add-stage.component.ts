import { Component, Input, OnInit } from '@angular/core';
import { Stage } from 'src/models/resource/stage';
import { ModalService } from 'src/services/modal/modal.service';
import { ProjectComponent } from '../project.component';

@Component({
  selector: 'app-add-stage',
  templateUrl: './add-stage.component.html',
  styleUrls: ['./add-stage.component.css']
})
export class AddStageComponent implements OnInit {

  @Input()public parent?:ProjectComponent;
  public newStage:Stage = new Stage("Новая стадия", "Моя новая стадия", this.defaultDeadline(), undefined, undefined, undefined);

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  addStage() {
    this.parent!.addStage(this.newStage);
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
