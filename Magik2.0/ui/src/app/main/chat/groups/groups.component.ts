import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Group } from 'src/models/chat/group';
import * as signalR from "@microsoft/signalr";
import { Profile } from 'src/models/resource/profile';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  @Input()currentProfile?:Profile;
  @Input() hub?:signalR.HubConnection;
  @Output()currentGroupSelected:EventEmitter<Group> = new EventEmitter<Group>();

  public groups?:Group[];
  public isGroupsShown: boolean = true;

  constructor() { }

  ngOnInit(): void {
    this.hub?.on("getGroups", (data) => {
      this.groups = data.map((name: string) => new Group(name));
    });
  }

  selectGroup(group:Group) {
    this.currentGroupSelected.emit(group);
  }

  createGroup(groupName:string) {
    let group = new Group(groupName);
    this.currentGroupSelected.emit(group);
  }
}
