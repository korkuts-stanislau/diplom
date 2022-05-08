import { Component, EventEmitter, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Attachment } from 'src/models/resource/attachment';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-create-attachment',
  templateUrl: './create-attachment.component.html',
  styleUrls: ['./create-attachment.component.css']
})
export class CreateAttachmentComponent implements OnInit {

  public newAttachment: Attachment = new Attachment("Новое вложение", "", 1);
  @Output() public attachmentCreated: EventEmitter<Attachment> = new EventEmitter<Attachment>();

  public rows:number = 3;
  public columns:number = 3;

  public table:string[][] = [["", "", ""], ["", "", ""], ["", "", ""]]
  public tableToCreate:string[][] = [["", "", ""], ["", "", ""], ["", "", ""]]

  constructor(public modalService: ModalService) { }

  reinitTable() {
    let newTable = new Array(this.rows)
    .fill("")
    .map(() => 
      new Array(this.columns).fill("")
    );
    for(let i = 0; i < Math.min(this.table.length, this.rows); i++) {
      for(let j = 0; j < Math.min(this.table[i].length, this.columns); j++) {
        newTable[i][j] = this.tableToCreate[i][j];
      }
    }
    this.table = JSON.parse(JSON.stringify(newTable));
    this.tableToCreate = JSON.parse(JSON.stringify(newTable));
  }

  changeTableValue(value:string, i:number, j:number) {
    this.tableToCreate[i][j] = value;
  }

  ngOnInit(): void {
  }

  createAttachment() {
    if(this.newAttachment.attachmentTypeId == 2) {
      this.newAttachment.data = JSON.stringify(this.tableToCreate);
    }
    this.attachmentCreated.emit(this.newAttachment);
    this.modalService.closeChild();
  }
}
