import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppHeader } from '../components/app-header/app-header.component';
import { VehicleCard } from '../components/vehicle-card/vehicle-card.component';
import { AppFooter } from '../components/app-footer/app-footer.component';
import { DataGrid } from '../components/data-grid/data-grid.component';
import { UploadWidget } from '../components/upload-widget/upload-widget.component';

@NgModule({
  declarations: [
    AppComponent,
    AppHeader,
    AppFooter,
    VehicleCard,
    DataGrid,
    UploadWidget
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
