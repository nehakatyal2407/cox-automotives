import { Component, OnDestroy } from '@angular/core';
import { NgRedux } from 'ng2-redux';
import { IAppState } from 'src/app/store';
import { UPLOAD_DATA, RESET_DATA, DATA_LOADING_STARTED, DATA_LOADING_ENDED } from './../../app/actions';
import { HttpDealService } from 'src/services/HttpDealService';
import { ToastrService } from 'ngx-toastr';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/forkJoin';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'upload-widget',
  templateUrl: './upload-widget.component.html',
  styleUrls: ['./upload-widget.component.scss']
})
export class UploadWidget implements OnDestroy {
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

    this.ngredux.dispatch({ type: DATA_LOADING_STARTED });
    // Step 1: Post Data on the server using Post Deals http call
    // As the data is posted in step 1, now we need to get data for all deals and top deals
    // Step 2: To get data from both the end points, I have combined this observable using forkJoin
    // I have subscribed to this combined observable and can access indiviual data using combined[0] and combined[1]
    this.subscription = this.dealService.PostDeals(this.formData).switchMap(
      (postData) => {
        console.log(`Data Posted on Server`);
        return Observable.forkJoin([
          this.dealService.getAllDeals(),
          this.dealService.getTopDeals()
        ])
      }
    ).subscribe(
      combined => this.ngredux.dispatch(
        {
          type: UPLOAD_DATA,
          body: {
            Deals: combined[0],
            TopDeals: combined[1]
          }
        }),
      error => {
        this.toastr.error("Error !! Validate Input Excel File");
        this.Reset();
      },
      () => {
        this.toastr.success("Data Uploaded Successfully");
        this.ngredux.dispatch({ type: DATA_LOADING_ENDED });
      }
    )

    // this.subscription = this.dealService.PostDeals(this.formData)
    //   .subscribe(
    //     data => this.ngredux.dispatch({ type: UPLOAD_DATA, body: data }),
    //     error => {
    //       this.toastr.error("Error !! Validate Input Excel File");
    //       this.Reset();
    //     },
    //     () => {
    //       this.toastr.success("Data Uploaded Successfully");
    //       this.ngredux.dispatch({ type: DATA_LOADING_ENDED });
    //     }
    //   )
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
