import { Component, OnInit } from '@angular/core';
import { Field } from 'src/models/resource/field';
import { Project } from 'src/models/resource/project';

@Component({
  selector: 'app-projects-manager',
  templateUrl: './projects-manager.component.html',
  styleUrls: ['./projects-manager.component.css']
})
export class ProjectsManagerComponent implements OnInit {

  public currentProject?:Project;

  constructor() { }

  ngOnInit(): void {
  }

  onCurrentProjectChanged(project:Project) {
    this.currentProject = project;
  }
}
