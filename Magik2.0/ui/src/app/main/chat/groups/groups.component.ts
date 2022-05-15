import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Group } from 'src/models/chat/group';
import { Profile } from 'src/models/resource/profile';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  @Input()currentProfile?:Profile;
  @Output()currentGroupSelected:EventEmitter<Group> = new EventEmitter<Group>();

  public groups?:Group[];
  public isGroupsShown: boolean = true;

  constructor() { }

  ngOnInit(): void {
    this.searchGroups();
    window.addEventListener('resize', (ev) => {
      if(window.innerWidth > 700) this.isGroupsShown = true;
    });
  }

  searchGroups() {
    this.groups = [new Group("C#"), 
                  new Group("Angular"), 
                  new Group("Typescript")]
  }

  selectGroup(group:Group) {
    this.currentGroupSelected.emit(group);
  }
}
