import { Component, Input, OnInit } from '@angular/core';
import { Project } from 'src/models/resource/project';

@Component({
  selector: 'app-stages',
  templateUrl: './stages.component.html',
  styleUrls: ['./stages.component.css']
})
export class StagesComponent implements OnInit {
  @Input() public currentProject?: Project;

  constructor() { }

  ngOnInit(): void {
  }

}
