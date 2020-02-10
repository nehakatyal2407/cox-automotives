// import * as angular from 'angular';
import { Component } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { IAppState } from 'src/app/store';
import { UPLOAD_DATA, RESET_DATA } from './../../app/actions';
import { HttpDealService } from 'src/services/HttpDealService';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'upload-widget',
  templateUrl: './upload-widget.component.html',
  styleUrls: ['./upload-widget.component.scss']
})
export class UploadWidget {
  file: any = {
    path : ''
  };

  formData = new FormData();
  constructor(private dealService: HttpDealService,private ngredux: NgRedux<IAppState>){
  }


  FilePathHandler(fileName: any) {
    console.log("Files :", fileName.target.files[0]);
    let fileToUpload = <File>fileName.target.files[0];
    this.formData.append('file', fileToUpload, fileToUpload.name);
    this.uploadData();  
  }

  uploadData() {
    this.dealService.PostDeals(this.formData).subscribe(
      (data) => {
        console.log(data);
        this.ngredux.dispatch({type: UPLOAD_DATA, body: data})
      }
    );
  }
  Reset()
  {
    this.file = {
      path : ''
    }; 
    this.ngredux.dispatch({type: RESET_DATA })
  }
}
