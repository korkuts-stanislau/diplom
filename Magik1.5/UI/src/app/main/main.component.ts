import { Component, OnInit } from '@angular/core';
import {RoutingService} from "../../services/routing/routing.service";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(public routing:RoutingService) { }

  ngOnInit(): void {
  }

}
