import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Group } from 'src/models/chat/group';
import * as signalR from "@microsoft/signalr";
import { Message } from 'src/models/chat/message';
import { Profile } from 'src/models/resource/profile';

@Component({
  selector: 'app-current-chat',
  templateUrl: './current-chat.component.html',
  styleUrls: ['./current-chat.component.css']
})
export class CurrentChatComponent implements OnInit, OnChanges {
  
  @Input()currentGroup?:Group;
  @Input() hub?:signalR.HubConnection;
  @Input()currentProfile?:Profile;
  isConnected:boolean = false;
  
  currentGroupMessages?:Message[];

  constructor(private sanitizer: DomSanitizer) { }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes.currentGroup) {
      if(changes.currentGroup.currentValue != undefined) {
        if(changes.currentGroup.previousValue != undefined) {
          if(changes.currentGroup.previousValue.name != changes.currentGroup.currentValue.name) {
            this.hub?.invoke("Disconnect", 
            changes.currentGroup.previousValue.name,
            this.currentProfile!.username).then(() => {
              this.hub?.invoke("Connect",
              changes.currentGroup.currentValue.name,
              this.currentProfile!.username).then(() => this.isConnected = true);
            });
          }
        }
        else {
          this.hub?.invoke("Connect",
          changes.currentGroup.currentValue.name,
          this.currentProfile!.username).then(() => this.isConnected = true);
        }
      }
    }
  }

  ngOnInit(): void {
    this.hub?.on("notify", (data) => {
      alert(data);
    });
    this.hub?.on("receive", (data) => {
      alert(data);
    });
  }

  ngOnDestroy(): void {
    if(this.isConnected) {
      this.hub?.invoke("Disconnect", 
      this.currentGroup!.name,
      this.currentProfile!.username);
    }
  }

  getPictureSource() {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${this.currentProfile?.icon}`);
  }
}
