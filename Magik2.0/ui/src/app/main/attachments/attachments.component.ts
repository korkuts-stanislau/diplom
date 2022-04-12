import { Component, Input, OnInit } from '@angular/core';
import { Stage } from 'src/models/resource/stage';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-attachments',
  templateUrl: './attachments.component.html',
  styleUrls: ['./attachments.component.css']
})
export class AttachmentsComponent implements OnInit {

  @Input() public stage?:Stage;

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  
}
