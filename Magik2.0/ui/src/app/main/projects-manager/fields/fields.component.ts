import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Field } from 'src/models/resource/field';
import { ModalService } from 'src/services/modal/modal.service';
import { FieldsService } from 'src/services/field/fields.service';

@Component({
  selector: 'app-fields',
  templateUrl: './fields.component.html',
  styleUrls: ['./fields.component.css']
})
export class FieldsComponent implements OnInit {

  public fields?:Field[];

  public currentField?: Field;

  @Output()changeFieldEvent = new EventEmitter<Field>();

  constructor(private sanitizer: DomSanitizer,
    public modalService: ModalService,
    private fieldsService: FieldsService) { }

  ngOnInit(): void {
    this.getFields();
  }

  selectField(field: Field) {
    this.currentField = this.currentField === field ? undefined : field;
    this.changeFieldEvent.emit(field);
  }

  getUrlFromIcon(icon:string) {
    if(icon == "") return "assets/puzzle.jpg";
    return  this.sanitizer.bypassSecurityTrustResourceUrl(
        `data:image/png;base64, ${icon}`);
  }

  getFields() {
    this.fieldsService.getFields()
      .subscribe(res => {
        this.fields = res;
      },err => {
        alert("Не удалось получить данные с сервера");
        console.log(err);
      });
  }

  addFieldModal() {
    this.modalService.openModal('add-field');
  }

  addField(field: Field) {
    this.fieldsService.addField(field)
      .subscribe(res => {
        field.id = res;
        this.fields!.push(field);
      }, err => {
        console.log(err);
        alert("Добавление не удалось");
      });
  }

  editFieldModal() {
    this.modalService.openModal('edit-field');
  }

  editField(field: Field) {
    this.fieldsService.editField(field)
      .subscribe(res => {
        
      }, err => {
        console.log(err);
        alert("Изменение не удалось");
      });
  }

  deleteField(field: Field) {
    if(confirm("Вы уверены что хотите удалить эту область проектов?")) {
      this.fieldsService.deleteField(field)
        .subscribe(res => {
          this.fields = this.fields?.filter(f => f !== field);
          this.currentField = undefined;
          this.changeFieldEvent.emit(this.currentField);
        }, err => {
          console.log(err);
          alert("Удаление не удалось");
        });
    }
  }
}
