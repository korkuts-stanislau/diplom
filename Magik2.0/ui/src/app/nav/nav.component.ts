import { Component, OnInit } from '@angular/core';
import {AppRoutingService} from "../../services/routing/routing.service";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public routing: AppRoutingService) { }

  ngOnInit(): void {
  }

}
