import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { Attachment } from 'src/models/resource/attachment';
import { Stage } from 'src/models/resource/stage';
import { AttachmentsService } from 'src/services/attachment/attachments.service';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-attachments',
  templateUrl: './attachments.component.html',
  styleUrls: ['./attachments.component.css']
})
export class AttachmentsComponent implements OnInit {

  @Input() public stage?:Stage;
  @Output() public attachmentAdded: EventEmitter<Attachment> = new EventEmitter<Attachment>();
  @Output() public attachmentEdited: EventEmitter<Attachment> = new EventEmitter<Attachment>();
  @Output() public attachmentDeleted: EventEmitter<Attachment> = new EventEmitter<Attachment>();
  public accountAttachments?: Attachment[];
  public attachToEdit?: Attachment;

  constructor(public modalService: ModalService,
              public attachmentsService: AttachmentsService) { }

  ngOnInit(): void {
    this.getAttachments();
  }

  getAttachments() {
    this.attachmentsService.getAttachments()
      .subscribe(res => {
        this.accountAttachments = res;
      }, err => {

      })
  }

  createAttachmentModal() {
    this.modalService.openChildModal("create-attachment");
  }

  createAttachment(attach:Attachment) {
    this.attachmentsService.createAttachment(attach)
      .subscribe(res => {
        this.accountAttachments?.push(res);
      }, err => {

      })
  }

  editAttachmentModal(attach: Attachment) {
    this.attachToEdit = attach;
    this.modalService.openChildModal("edit-attachment");
  }

  editAttachment(attach:Attachment) {
    this.attachmentsService.editAttachment(attach)
    .subscribe(res => {
      this.attachmentEdited.emit(res);
      let i = this.accountAttachments?.indexOf(this.attachToEdit!);
      this.accountAttachments![i!] = res;
    }, err => {

    });
  }

  deleteAttachment(attach: Attachment) {
    if(confirm("Вы точно хотите удалить это вложение?")) {
      this.attachmentsService.deleteAttachment(attach)
        .subscribe(res => {
          this.attachmentDeleted.emit(attach);
          this.accountAttachments = this.accountAttachments!.filter(a => a.id != attach.id);
        }, err => {

        });
    }
  }

  addAttachmentToStage(attach: Attachment) {
    this.attachmentsService.addAttachmentToStage(this.stage!, attach)
      .subscribe(res => {
        this.attachmentAdded.emit(attach);
      }, err => {
        alert("Вложение уже находится в этой стадии");
      });
  }
}
