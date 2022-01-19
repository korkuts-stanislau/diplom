import { Component, OnInit } from '@angular/core';
import {RoutingService} from "../../services/routing/routing.service";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public routing: RoutingService) { }

  ngOnInit(): void {
  }

}
