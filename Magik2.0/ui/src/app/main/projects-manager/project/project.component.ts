import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Project } from 'src/models/resource/project';
import { Stage } from 'src/models/resource/stage';
import { ModalService } from 'src/services/modal/modal.service';
import { ProjectsService } from 'src/services/project/projects.service';
import { StagesService } from 'src/services/stage/stages.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  @Input() public currentProject?: Project;
  @Output() public currentProjectDeleted: EventEmitter<any> = new EventEmitter<any>();
  @Output() public stageAdded: EventEmitter<Stage> = new EventEmitter<Stage>();

  public descriptionShown: boolean = false;

  constructor(private projectsService: ProjectsService,
              private stagesService: StagesService,
              public modalService: ModalService) { }

  ngOnInit(): void {
  }

  addStageModal() {
    this.modalService.openModal('add-stage');
  }

  addStage(stage: Stage) {
    this.stagesService.addStage(this.currentProject!, stage)
      .subscribe(res => {
        this.stageAdded.emit(res);
      }, err => {
        console.log(err);
        alert("Изменение не удалось");
      });
  }

  editCurrentProjectModal() {
    this.modalService.openModal('edit-project');
  }

  editProject(project: Project) {
    this.projectsService.editProject(project)
      .subscribe(res => {
        
      }, err => {
        console.log(err);
        alert("Изменение не удалось");
      });
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
}
