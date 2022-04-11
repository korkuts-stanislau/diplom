import { Component, OnInit, ViewChild } from '@angular/core';
import { Project } from 'src/models/resource/project';
import { Stage } from 'src/models/resource/stage';
import { FieldsComponent } from './fields/fields.component';
import { StagesComponent } from './stages/stages.component';

@Component({
  selector: 'app-projects-manager',
  templateUrl: './projects-manager.component.html',
  styleUrls: ['./projects-manager.component.css']
})
export class ProjectsManagerComponent implements OnInit {

  @ViewChild(FieldsComponent)private fields?:FieldsComponent;
  @ViewChild(StagesComponent)private stages?:StagesComponent;
  
  public currentProject?:Project;

  constructor() { }

  ngOnInit(): void {
  }

  onCurrentProjectChanged(project:Project) {
    this.currentProject = project;
  }

  onCurrentProjectDeleted(item:any) {
    this.fields?.removeProjectFromList(this.currentProject!);
    this.currentProject = undefined;
  }

  onStageAdded(stage:Stage) {
    this.stages?.addStageToList(stage);
    this.fields?.getCurrentFieldProjects();
  }
}
