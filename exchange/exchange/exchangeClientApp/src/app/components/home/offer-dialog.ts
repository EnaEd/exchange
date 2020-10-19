import { IAppState } from './../../store/app.state';
import { FileUploadModel } from './../../Models/file-upload.model';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  HttpRequest,
  HttpClient,
  HttpEventType,
  HttpErrorResponse,
} from '@angular/common/http';
import { tap, last, catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { Store } from '@ngrx/store';
import * as HomeActions from './store/home.actions';

@Component({
  selector: 'offerDialog',
  templateUrl: 'offer-dialog.html',
})
export class OfferDialog {
  @Input() text = 'Upload';

  @Input() param = 'file';

  @Input() target = 'https://storage.googleapis.com/exchangemedia/';
  @Input() accept = 'image/*';
  @Output() complete = new EventEmitter<string>();
  public currentFile: FileUploadModel;
  constructor(
    private _store: Store<IAppState>,
    private _dialogRef: MatDialogRef<OfferDialog>,
    private _http: HttpClient
  ) {}
  public form: FormGroup = new FormGroup({
    description: new FormControl('', Validators.required),
  });

  Close() {
    this._store.dispatch(HomeActions.ClearFileForUploadAction());
    this._dialogRef.close();
  }
  AddOffer() {
    if (this.currentFile) {
      const reader = new FileReader();
      reader.readAsDataURL(this.currentFile.data);
      reader.onload = () => {
        console.log(reader.result);
      };
    }
    //TODO create upload offer request model for send to server
    //this._store.dispatch()
  }

  Upload() {
    const fileUpload = document.getElementById(
      'fileUpload'
    ) as HTMLInputElement;
    fileUpload.onchange = () => {
      const file = fileUpload.files[fileUpload.files.length - 1];
      this.currentFile = {
        data: file,
        state: 'in',
        inProgress: false,
        progress: 0,
        canRetry: false,
        canCancel: true,
      };

      this._store.dispatch(
        HomeActions.GetFileForUploadAction({ payload: this.currentFile })
      );
    };
    fileUpload.click();
  }

  deleteFile() {
    this.currentFile = null;
    this._store.dispatch(HomeActions.ClearFileForUploadAction());
  }
}
