import { Component, Input, OnInit } from '@angular/core';
import { Project } from 'src/models/resource/project';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  @Input() public currentProject?: Project;

  constructor() { }

  ngOnInit(): void {
  }

}
