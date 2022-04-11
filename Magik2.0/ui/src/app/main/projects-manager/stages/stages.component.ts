import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Project } from 'src/models/resource/project';
import { Stage } from 'src/models/resource/stage';
import { StagesService } from 'src/services/stage/stages.service';

@Component({
  selector: 'app-stages',
  templateUrl: './stages.component.html',
  styleUrls: ['./stages.component.css']
})
export class StagesComponent implements OnInit, OnChanges {
  @Input() public currentProject?: Project;
  public stages?:Stage[];

  public openedStages: Stage[] = new Array<Stage>();

  constructor(private stagesService: StagesService) { }

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
}
