import { Component, OnInit } from '@angular/core';
import { Field } from 'src/models/resource/field';

@Component({
  selector: 'app-projects-manager',
  templateUrl: './projects-manager.component.html',
  styleUrls: ['./projects-manager.component.css']
})
export class ProjectsManagerComponent implements OnInit {
  public currentField?: Field;

  constructor() { }

  ngOnInit(): void {
  }

  changeField(field: Field) {
    this.currentField = field;
  }
}
