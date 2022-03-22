import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Field } from 'src/models/resource/field';
import { Project } from 'src/models/resource/project';
import { ProjectsService } from 'src/services/project/projects.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit, OnChanges {

  @Input()currentField?: Field;
  projects?: Project[];

  constructor(private projectsService: ProjectsService) { }
  
  ngOnChanges(changes: SimpleChanges): void {
    this.projectsService.getProjects(changes["currentField"].currentValue)
      .subscribe(res => {
        this.projects = res;
      }, err => {
        console.log(err);
        alert("Не удалось получить проекты для этой области");
      });
  }

  ngOnInit(): void {

  }
}
