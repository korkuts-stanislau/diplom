import {Component, Input, OnChanges, OnInit, Output, SimpleChanges, EventEmitter} from '@angular/core';
import {Project} from "../../../models/project";
import {ProjectsService} from "../../../services/projects/projects.service";

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit, OnChanges {
  @Input() currentProject?: Project;
  @Input() isEditProject?: boolean;

  progress?: number;

  constructor(private projectsService: ProjectsService) { }

  ngOnInit(): void {
    this.getProgress();
  }

  ngOnChanges(changes: SimpleChanges): void {

  }

  getProgress() {
    this.projectsService.getProjectProgress(this.currentProject!)
      .subscribe(res => {
        this.progress = res;
      }, err => {
        console.log(err);
      });
  }
}
