import { Component, OnDestroy } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { IAppState } from 'src/app/store';
import { UPLOAD_DATA, RESET_DATA } from './../../app/actions';
import { HttpDealService } from 'src/services/HttpDealService';
import { ToastrService } from 'ngx-toastr';
import 'rxjs/add/operator/catch';

@Component({
  selector: 'upload-widget',
  templateUrl: './upload-widget.component.html',
  styleUrls: ['./upload-widget.component.scss']
})
export class UploadWidget implements OnDestroy{
  file: any = {
    path: ''
  };

  formData = new FormData();
  subscription;
  constructor(private dealService: HttpDealService, private ngredux: NgRedux<IAppState>, private toastr: ToastrService) {
  }


  FilePathHandler(fileName: any) {
    console.log("Files :", fileName.target.files[0]);
    let fileToUpload = <File>fileName.target.files[0];
    this.formData.append('file', fileToUpload, fileToUpload.name);
    this.uploadData();
  }

  uploadData() {
    this.subscription = this.dealService.PostDeals(this.formData)
    .subscribe(
      data => this.ngredux.dispatch({ type: UPLOAD_DATA, body: data }),
      error => {
        this.toastr.error("Server Error has Occured, Please contact Administrator");
        this.Reset();
      },
      () => this.toastr.success("Data Uploaded Successfully")
    )
  }
  Reset() {
    this.file = {
      path: ''
    };
    this.ngredux.dispatch({ type: RESET_DATA })
  }

  ngOnDestroy(): void {
      this.subscription.unsubscribe();
  }
}
