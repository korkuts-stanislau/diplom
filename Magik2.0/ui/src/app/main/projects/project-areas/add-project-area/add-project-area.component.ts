import { Component, OnInit } from '@angular/core';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-add-project-area',
  templateUrl: './add-project-area.component.html',
  styleUrls: ['./add-project-area.component.css']
})
export class AddProjectAreaComponent implements OnInit {

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }
}
