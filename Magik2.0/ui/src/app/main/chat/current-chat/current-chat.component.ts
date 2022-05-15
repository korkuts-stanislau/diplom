import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Group } from 'src/models/chat/group';
import { Message } from 'src/models/chat/message';
import { Profile } from 'src/models/resource/profile';

@Component({
  selector: 'app-current-chat',
  templateUrl: './current-chat.component.html',
  styleUrls: ['./current-chat.component.css']
})
export class CurrentChatComponent implements OnInit, OnChanges {
  @Input()currentGroup?:Group;
  @Input()currentProfile?:Profile;
  
  currentGroupMessages?:Message[];

  constructor(private sanitizer: DomSanitizer) { }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes.currentGroup) {
      this.initCurrentGroup();
    }
  }

  ngOnInit(): void {
  }

  getPictureSource() {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${this.currentProfile?.icon}`);
  }

  initCurrentGroup() {

  }
}
