import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Attachment } from 'src/models/resource/attachment';
import { Stage } from 'src/models/resource/stage';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-edit-attachment',
  templateUrl: './edit-attachment.component.html',
  styleUrls: ['./edit-attachment.component.css']
})
export class EditAttachmentComponent implements OnInit {
  @Input() public stage?: Stage;
  @Input() public attachment?: Attachment;
  @Output() public attachmentEdited: EventEmitter<Attachment> = new EventEmitter<Attachment>();

  public rows:number = 3;
  public columns:number = 3;

  public table:string[][] = [["", "", ""], ["", "", ""], ["", "", ""]]
  public tableToUpdate:string[][] = [["", "", ""], ["", "", ""], ["", "", ""]]

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
    if(this.attachment?.attachmentTypeId == 2) {
      this.table = JSON.parse(this.attachment!.data);
      this.tableToUpdate = JSON.parse(this.attachment!.data);
      this.rows = this.table.length;
      this.columns = this.table[0].length;
    }
  }

  reinitTable() {
    let newTable = new Array(this.rows)
    .fill("")
    .map(() => 
      new Array(this.columns).fill("")
    );
    for(let i = 0; i < Math.min(this.table.length, this.rows); i++) {
      for(let j = 0; j < Math.min(this.table[i].length, this.columns); j++) {
        newTable[i][j] = this.tableToUpdate[i][j];
      }
    }
    this.table = JSON.parse(JSON.stringify(newTable));
    this.tableToUpdate = JSON.parse(JSON.stringify(newTable));
  }

  changeTableValue(value:string, i:number, j:number) {
    this.tableToUpdate[i][j] = value;
  }

  editAttachment() {
    if(this.attachment!.attachmentTypeId == 2) {
      this.attachment!.data = JSON.stringify(this.tableToUpdate);
    }
    this.attachmentEdited.emit(this.attachment);
    this.modalService.closeChild();
  }
}
