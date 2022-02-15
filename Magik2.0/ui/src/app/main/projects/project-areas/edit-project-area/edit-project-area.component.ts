import { Component, Input, OnInit } from '@angular/core';
import { ModalService } from 'src/services/modal/modal.service';
import { ProjectAreasComponent } from '../project-areas.component';

@Component({
  selector: 'app-edit-project-area',
  templateUrl: './edit-project-area.component.html',
  styleUrls: ['./edit-project-area.component.css']
})
export class EditProjectAreaComponent implements OnInit {

  @Input()parent?:ProjectAreasComponent;

  constructor(public modalService:ModalService) { }

  ngOnInit(): void {
  }

}
