import { Component } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { IAppState } from 'src/app/store';

@Component({
  selector: 'vehicle-card',
  templateUrl: './vehicle-card.component.html',
  styleUrls: ['./vehicle-card.component.scss']
})
export class VehicleCard {
  deals: string[];
  constructor(private ngredux: NgRedux<IAppState>){
    ngredux.subscribe(() => {
      this.deals = ngredux.getState().TopDeals;
    });
  }

}
