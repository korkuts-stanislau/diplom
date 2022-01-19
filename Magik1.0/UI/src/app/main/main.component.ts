import { Component, OnInit } from '@angular/core';
import {AuthService} from "../services/auth/auth.service";
import {ProfileService} from "../services/profile/profile.service";
import {Profile} from "../models/profile";
import {ProjectArea} from "../models/projectArea";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  currentProjectArea?: ProjectArea;

  constructor() { }

  ngOnInit(): void {}

  changeProjectArea(projectArea: ProjectArea) {
    this.currentProjectArea = projectArea;
  }
}
