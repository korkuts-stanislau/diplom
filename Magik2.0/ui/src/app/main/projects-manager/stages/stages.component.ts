import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Project } from 'src/models/resource/project';
import { Stage } from 'src/models/resource/stage';
import { ModalService } from 'src/services/modal/modal.service';
import { StagesService } from 'src/services/stage/stages.service';

@Component({
  selector: 'app-stages',
  templateUrl: './stages.component.html',
  styleUrls: ['./stages.component.css']
})
export class StagesComponent implements OnInit, OnChanges {
  @Input() public currentProject?: Project;
  @Output() public stageEditedOrDeleted: EventEmitter<any> = new EventEmitter<any>()
  public stages?:Stage[];
  public stageToEdit?:Stage;
  public stageToAddAttachment?:Stage;

  public openedStages: Stage[] = new Array<Stage>();


  constructor(private stagesService: StagesService,
              public modalService: ModalService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes["currentProject"]) this.getStages();
  }

  ngOnInit(): void {
    this.getStages();
  }

  isStageOpened(stage:Stage) {
    return this.openedStages.find(s => s.id == stage.id) != undefined;
  }

  openStage(stage:Stage) {
    this.openedStages.push(stage);
  }

  closeStage(stage:Stage) {
    this.openedStages = this.openedStages.filter(s => s.id != stage.id);
  }

  getStages() {
    this.stagesService.getStages(this.currentProject!)
      .subscribe(res => {
        this.stages = res;
      }, err => {
        console.log(err);
        alert(err);
      });
  }

  addStageToList(stage:Stage) {
    this.stages?.push(stage);
  }

  editStageModal(stage:Stage) {
    this.stageToEdit = stage;
    this.modalService.openModal('edit-stage');
  }

  editStage(stage: Stage) {
    this.stagesService.editStage(stage)
      .subscribe(res => {
        let i = this.stages?.findIndex(s => s.id == stage.id)!;
        this.stages![i] = res;
        this.stageEditedOrDeleted.emit();
      }, err => {
        console.log(err);
        alert("Изменение не удалось");
      });
  }

  deleteStage(stage:Stage) {
    if(confirm("Вы уверены что хотите удалить эту стадию?")) {
      this.stagesService.deleteStage(stage)
        .subscribe(res => {
          this.stages = this.stages?.filter(s => s.id != stage.id);
          this.stageEditedOrDeleted.emit();
        }, err => {
          console.log(err);
          alert("Удаление не удалось");
        });
    }
  }

  addAttachmentModal(stage:Stage) {
    this.stageToAddAttachment = stage;
    this.modalService.openModal("attachments");
  }
}
