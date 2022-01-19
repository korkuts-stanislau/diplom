import {Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges} from '@angular/core';
import {Project} from "../../../../models/project";
import {ProjectPartsService} from "../../../../services/projects/project-parts/project-parts.service";
import {ProjectPart} from "../../../../models/projectPart";

@Component({
  selector: 'app-project-parts',
  templateUrl: './project-parts.component.html',
  styleUrls: ['./project-parts.component.css']
})
export class ProjectPartsComponent implements OnInit, OnChanges {
  @Input() currentProject?: Project;
  @Output() progressChangeEmitter = new EventEmitter<any>();

  projectParts?: Array<ProjectPart>;

  currentProjectPart?: ProjectPart;

  isEditProjectPart: boolean = false;

  constructor(private projectPartsService: ProjectPartsService) { }

  ngOnInit(): void {
    this.getProjectParts();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.getProjectParts();
  }

  getProjectParts() {
    this.projectPartsService.getProjectPartsForProject(this.currentProject!)
      .subscribe(res => {
        this.projectParts = res;
      }, err => {
        console.log(err);
      });
  }

  addProjectPart() {
    this.projectPartsService.addProjectPart(new ProjectPart(0,
      "Новая часть проекта",
      this.currentProject!.id,
      ""))
      .subscribe(res => {
        this.projectParts!.push(res);
      }, err => {
        console.log(err);
      });
  }

  editProjectPartDetails(projectPart: ProjectPart) {
    this.isEditProjectPart = !this.isEditProjectPart; // Что бы изменение описание не поменяло h3 на input
    this.editProjectPart(projectPart);
  }

  editProjectPart(projectPart: ProjectPart) {
    this.currentProjectPart = projectPart;
    this.isEditProjectPart = !this.isEditProjectPart;
    if(!this.isEditProjectPart) {
      this.projectPartsService.editProjectPart(projectPart)
        .subscribe(res => {
          this.progressChangeEmitter.emit();
        }, err => {
          console.log(err);
        });
    }
  }

  deleteProjectPart(projectPart: ProjectPart) {
    if(confirm("Вы действительно хотите удалить эту часть проекта?")) {
      this.projectPartsService.deleteProjectPart(projectPart)
        .subscribe(res => {
          this.getProjectParts();
        }, err => {
          console.log(err);
        });
    }
  }

}
