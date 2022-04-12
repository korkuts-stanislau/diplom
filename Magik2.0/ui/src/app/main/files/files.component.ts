import { Component, Input, OnInit } from '@angular/core';
import { Stage } from 'src/models/resource/stage';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {
  @Input() public stage?: Stage;

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

}
