import { Component, ElementRef, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Group } from 'src/models/chat/group';
import * as signalR from "@microsoft/signalr";
import { Message } from 'src/models/chat/message';
import { Profile } from 'src/models/resource/profile';
import { ProfilesService } from 'src/services/profile/profiles.service';
import { ModalService } from 'src/services/modal/modal.service';

@Component({
  selector: 'app-current-chat',
  templateUrl: './current-chat.component.html',
  styleUrls: ['./current-chat.component.css']
})
export class CurrentChatComponent implements OnInit, OnChanges, OnDestroy {
  
  @Input()currentGroup?:Group;
  @Input()hub?:signalR.HubConnection;
  @Input()currentProfile?:Profile;
  isConnected:boolean = false;
  
  currentGroupMessages:Array<Message|string> = new Array<Message|string>();
  currentMessageText:string = "";

  @ViewChild('chatMessages') private chatMessages?: ElementRef;

  constructor(private sanitizer: DomSanitizer,
              private profilesService:ProfilesService,
              public modalService:ModalService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes.currentGroup) {
      if(changes.currentGroup.currentValue != undefined) {
        if(changes.currentGroup.previousValue != undefined) {
          if(changes.currentGroup.previousValue.name != changes.currentGroup.currentValue.name) {
            this.hub?.invoke("Disconnect", 
              changes.currentGroup.previousValue.name,
              this.currentProfile!.username).then(() => {
                this.currentGroupMessages = [];
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
    let me = this;
    window.addEventListener('beforeunload', function (e) {
      e.preventDefault();
      if(me.isConnected) {
        me.hub?.invoke("Disconnect", 
          me.currentGroup?.name,
          me.currentProfile!.username);
        me.isConnected = false;
      }
    });
    this.hub?.on("notify", (data) => {
      this.currentGroupMessages?.push(data as string);
      this.scrollToBottom();
    });
    this.hub?.on("receive", (data) => {
      this.currentGroupMessages?.push(data as Message);
      this.scrollToBottom();
    });
  }

  ngOnDestroy(): void {
    if(this.isConnected) {
      this.currentGroupMessages = [];
      this.hub?.invoke("Disconnect", 
        this.currentGroup!.name,
        this.currentProfile!.username);
    }
  }

  getPictureSource(icon:string) {
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `data:image/png;base64, ${icon}`);
  }

  sendMessage() {
    if(this.currentMessageText.length < 1) {
      alert("Нельзя отправить пустое сообщение");
      return;
    }
    if(!this.isConnected) {
      alert("Сначала подключитесь к группе");
      return;
    }
    let newMessage = new Message(this.currentProfile!.id, 
                                 this.currentProfile!.username,
                                 new Date(),
                                 this.currentMessageText);
    this.hub?.invoke("Send", this.currentGroup?.name, newMessage)
      .then(() => {
        this.currentMessageText = "";
        this.scrollToBottom();
      });
  }

  isNotifyMessage(message:string|Message) {
    return typeof message == 'string';
  }

  getStrictMessage(message:string|Message): Message {
    return message as Message;
  }

  getClassForMessage(message:string|Message) {
    if((message as Message).profileId == this.currentProfile?.id) {
      return "message right-message";
    }
    else {
      return "message left-message";
    }
  }

  inputKeyPressed(event:Event) {
    if((event as KeyboardEvent).key === "Enter") {
      event.preventDefault();
      this.sendMessage();
    }
  }

  scrollToBottom(): void {
    setTimeout(() => {
      this.chatMessages!.nativeElement.scrollTop = this.chatMessages!.nativeElement.scrollHeight;       
    }, 50);
}

  getPrettyDate(date:Date):string {
    date = new Date(date);
    if(this.isToday(date)) {
      return String(date.getHours()).padStart(2, '0') + ":" + String(date.getMinutes()).padStart(2, '0');
    }
    else {
      return String(date.getHours()).padStart(2, '0') + ":" + String(date.getMinutes()).padStart(2, '0') + " " + date.getDate() + "/" + date.getMonth() + "/" + date.getFullYear();
    }
  }

  isToday = (someDate:Date) => {
    const today = new Date();
    return someDate.getDate() == today.getDate() &&
      someDate.getMonth() == today.getMonth() &&
      someDate.getFullYear() == today.getFullYear();
  }

  openedProfile?: Profile;
  openProfileModal(profileId:number) {
    let mockProfile = new Profile(profileId, "", "");
    this.profilesService.getOtherProfile(mockProfile)
      .subscribe(res => {
        this.openedProfile = res;
        this.modalService.openModal('show-profile');
      }, err => console.log(err));
  }
}
