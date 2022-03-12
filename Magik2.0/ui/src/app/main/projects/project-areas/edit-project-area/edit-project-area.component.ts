import { Component, Input, OnInit } from '@angular/core';
import { ProjectArea } from 'src/models/projects/projectArea';
import { ModalService } from 'src/services/modal/modal.service';
import { ProjectAreasComponent } from '../project-areas.component';

@Component({
  selector: 'app-edit-project-area',
  templateUrl: './edit-project-area.component.html',
  styleUrls: ['./edit-project-area.component.css']
})
export class EditProjectAreaComponent implements OnInit {

  @Input()parent?:ProjectAreasComponent;
  @Input()areaToEdit?:ProjectArea;

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  editProjectArea() {
    this.parent!.editProjectArea(this.areaToEdit!);
    this.modalService.closeModal();
  }

  editPhotoEvent(event: Event) {
    let me = this;
    let input = event.target as HTMLInputElement;
    if(input.files) {
      const file = input.files[0];
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
         // get only data part from base64
        me.areaToEdit!.icon = reader.result!.toString().split(',')[1];
      };
    }
  }
}
