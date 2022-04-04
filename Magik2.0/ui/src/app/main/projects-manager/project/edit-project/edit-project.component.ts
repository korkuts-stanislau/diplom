import { Component, Input, OnInit } from '@angular/core';
import { Project } from 'src/models/resource/project';
import { ModalService } from 'src/services/modal/modal.service';
import { ProjectComponent } from '../project.component';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css']
})
export class EditProjectComponent implements OnInit {

  @Input()public parent?:ProjectComponent;
  @Input()public projectToEdit?:Project;

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  editProject() {
    this.parent!.editProject(this.projectToEdit!);
    this.modalService.closeModal();
  }
}
