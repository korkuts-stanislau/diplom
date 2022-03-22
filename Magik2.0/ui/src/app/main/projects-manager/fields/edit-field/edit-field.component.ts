import { Component, Input, OnInit } from '@angular/core';
import { Field } from 'src/models/resource/field';
import { ModalService } from 'src/services/modal/modal.service';
import { FieldsComponent } from '../fields.component';

@Component({
  selector: 'app-edit-field',
  templateUrl: './edit-field.component.html',
  styleUrls: ['./edit-field.component.css']
})
export class EditFieldComponent implements OnInit {

  @Input()parent?:FieldsComponent;
  @Input()fieldToEdit?:Field;

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  editField() {
    this.parent!.editField(this.fieldToEdit!);
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
        me.fieldToEdit!.icon = reader.result!.toString().split(',')[1];
      };
    }
  }
}
