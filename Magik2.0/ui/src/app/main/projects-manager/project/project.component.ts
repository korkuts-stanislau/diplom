import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Project } from 'src/models/resource/project';
import { ProjectsService } from 'src/services/project/projects.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  @Input() public currentProject?: Project;
  @Output() public currentProjectDeleted: EventEmitter<any> = new EventEmitter<any>();

  constructor(private projectsService: ProjectsService) { }

  ngOnInit(): void {
  }

  deleteCurrentProject() {
    if(confirm("Вы уверены что хотите удалить этот проект?")) {
      this.projectsService.deleteProject(this.currentProject!)
        .subscribe(res => {
          this.currentProjectDeleted.emit();
        }, err => {
          console.log(err);
          alert("Удаление не удалось");
        });
    }
  }

  editCurrentProjectModal() {

  }
}
