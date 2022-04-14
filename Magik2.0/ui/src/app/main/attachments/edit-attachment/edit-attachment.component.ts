import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Attachment } from 'src/models/resource/attachment';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-edit-attachment',
  templateUrl: './edit-attachment.component.html',
  styleUrls: ['./edit-attachment.component.css']
})
export class EditAttachmentComponent implements OnInit {

  @Input() public attachment?: Attachment;
  @Output() public attachmentEdited: EventEmitter<Attachment> = new EventEmitter<Attachment>();

  constructor(public modalService: ModalService) { }

  ngOnInit(): void {
  }

  editAttachment() {
    this.attachmentEdited.emit(this.attachment);
    this.modalService.closeChild();
  }
}
