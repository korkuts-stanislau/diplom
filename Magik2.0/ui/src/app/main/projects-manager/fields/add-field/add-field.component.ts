import { Component, Input, OnInit } from '@angular/core';
import { Field } from 'src/models/resource/field';
import { ModalService } from 'src/services/modal/modal.service';
import { FieldsComponent } from '../fields.component';

@Component({
  selector: 'app-add-field',
  templateUrl: './add-field.component.html',
  styleUrls: ['./add-field.component.css']
})
export class AddFieldComponent implements OnInit {

  @Input()parent?:FieldsComponent;

  public newField:Field = new Field(0, "Новая область", "");

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  addNewField() {
    this.parent!.addField(this.newField);
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
        me.newField!.icon = reader.result!.toString().split(',')[1];
      };
    }
  }
}
