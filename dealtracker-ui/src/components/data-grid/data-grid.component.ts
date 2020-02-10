import { Component, ViewChild, OnInit } from '@angular/core';
import { NgRedux, select } from 'ng2-redux';
import { IAppState } from 'src/app/store';
import { Deal } from 'src/interfaces/Deal';
import {MatTableDataSource} from '@angular/material/table';
import { MatSpinner } from '@angular/material';

@Component({
  selector: 'data-grid',
  templateUrl: './data-grid.component.html',
  styleUrls: ['./data-grid.component.scss']
})
export class DataGrid {

  deals: Deal[];
  isLoading: boolean = false;
  empty: boolean = true;
  displayedColumns: string[] = ['dealNumber', 'customerName', 'dealershipName', 'vehicle','price','date'];
  dataSource = new MatTableDataSource<Deal>([]);

  constructor(private ngredux: NgRedux<IAppState>){
    ngredux.subscribe(() => {
      this.deals = ngredux.getState().Deals;
      this.dataSource = new MatTableDataSource<Deal>(ngredux.getState().Deals);
      this.empty = false;
      this.isLoading = ngredux.getState().isLoading;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}
