import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {ProjectArea} from "../../models/projectArea";
import {ProjectsService} from "../../services/projects/projects.service";
import {Project} from "../../models/project";

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit, OnChanges {
  @Input() currentProjectArea?: ProjectArea;
  projects?: Array<Project>;

  currentProject?: Project;

  isCreateProject: boolean = false;
  isEditProject: boolean = false;

  createProjectName?: string;
  createProjectDescription?: string;

  constructor(private projectsService: ProjectsService) { }

  ngOnInit(): void {
    this.getProjects();
  }

  ngOnChanges(changes: SimpleChanges) {
    this.getProjects();
  }

  getProjects() {
    this.projectsService.getProjectsForProjectArea(this.currentProjectArea!)
      .subscribe(res => {
        this.projects = res;
        let isEmpty: boolean = this.projects!.length == 0;
        this.isCreateProject = isEmpty;
        if(isEmpty) {
          this.currentProject = undefined;
        }
        else {
          this.currentProject = this.projects![0];
        }
      }, err => {
        console.log(err);
      });
  }

  createProject() {
    this.projectsService.createProject(new Project(0,
      this.createProjectName!,
      this.createProjectDescription!,
      this.currentProjectArea!.id))
      .subscribe(res => {
        this.projects!.push(res);
        this.isCreateProject = false;
        this.currentProject = res;
      }, err => {
        console.log(err);
      });
  }

  deleteProject() {
    if(confirm("Вы действительно хотите удалить этот проект?")) {
      this.projectsService.deleteProject(this.currentProject!)
        .subscribe(res => {
          this.getProjects();
        }, err => {
          console.log(err);
        });
    }
  }

  editProject() {
    this.isEditProject = !this.isEditProject;
    if(!this.isEditProject) {
      this.projectsService.editProject(this.currentProject!)
        .subscribe(res => {
          /*проект изменён*/
        }, err => {
          console.log(err);
        });
    }
  }

}
