import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { Project } from 'src/models/resource/project';
import { Stage } from 'src/models/resource/stage';
import { FieldsService } from 'src/services/field/fields.service';
import { ProjectsService } from 'src/services/project/projects.service';
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

  constructor(private projectsService: ProjectsService) { }

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
    this.reloadProject();
  }

  onStageEditedOrDeleted() {
    this.reloadProject();
  }
  
  reloadProject() {
    this.projectsService.getProject(this.currentProject?.id!)
      .subscribe(res => {
        this.currentProject!.name = res.name;
        this.currentProject!.description = res.description;
        this.currentProject!.color = res.color;
      }, err => {
        
      })
  }
}
