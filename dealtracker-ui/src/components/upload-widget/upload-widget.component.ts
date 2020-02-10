// import * as angular from 'angular';
import { Component } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { IAppState } from 'src/app/store';
import { UPLOAD_DATA } from './../../app/actions';
import { HttpDealService } from 'src/services/HttpDealService';

@Component({
  selector: 'upload-widget',
  templateUrl: './upload-widget.component.html',
  styleUrls: ['./upload-widget.component.scss']
})
export class UploadWidget {
  formData = new FormData();
  constructor(private dealService: HttpDealService,private ngredux: NgRedux<IAppState>){
  }

  FilePathHandler(fileName) {
    console.log("Files :", fileName.target.files[0]);
    let fileToUpload = <File>fileName.target.files[0];
    this.formData.append('file', fileToUpload, fileToUpload.name);
  }

  uploadData() {
    this.dealService.PostDeals(this.formData).subscribe(
      (data) => {
        console.log(data);
        this.ngredux.dispatch({type: UPLOAD_DATA, body: data})
      }
    );
  }
}
