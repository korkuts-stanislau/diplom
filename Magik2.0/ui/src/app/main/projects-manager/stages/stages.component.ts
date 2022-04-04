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

  constructor(private stagesService: StagesService) { }

  ngOnChanges(changes: SimpleChanges): void {
    this.getStages();
  }

  ngOnInit(): void {
    this.getStages();
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
}
