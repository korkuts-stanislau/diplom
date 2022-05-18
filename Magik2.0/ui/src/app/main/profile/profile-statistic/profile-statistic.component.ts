import { Component, OnInit } from '@angular/core';
import { Statistic } from 'src/models/resource/statistic';
import { StatisticService } from 'src/services/statistic/statistic.service';

@Component({
  selector: 'app-profile-statistic',
  templateUrl: './profile-statistic.component.html',
  styleUrls: ['./profile-statistic.component.css']
})
export class ProfileStatisticComponent implements OnInit {

  public statistic?:Statistic;

  constructor(private statisticService:StatisticService) { }

  ngOnInit(): void {
    this.statisticService.get()
      .subscribe(res => {
        this.statistic = res;
      }, err => console.log(err));
  }

}
