import { Component, Input, OnInit } from '@angular/core';
import { ProjectArea } from 'src/models/projects/projectArea';
import { ModalService } from 'src/services/modal/modal.service';
import { ProjectAreasComponent } from '../project-areas.component';

@Component({
  selector: 'app-add-project-area',
  templateUrl: './add-project-area.component.html',
  styleUrls: ['./add-project-area.component.css']
})
export class AddProjectAreaComponent implements OnInit {

  @Input()parent?:ProjectAreasComponent;

  public newArea:ProjectArea = new ProjectArea(0, "Новая область", "");

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  addNewProjectArea() {
    this.parent!.addProjectArea(this.newArea);
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
        me.newArea!.icon = reader.result!.toString().split(',')[1];
      };
    }
  }
}
