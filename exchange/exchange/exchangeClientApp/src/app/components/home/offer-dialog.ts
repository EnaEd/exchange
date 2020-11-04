import { userSelector } from './../auth/store/auth.selectors';
import { UserModel } from './../../Models/user.model';
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
import { Store, select } from '@ngrx/store';
import * as HomeActions from './store/home.actions';
import { UploadOfferRequestModel } from 'src/app/Models/RequestModels/upload-offer-request.model';
import { OfferOwnerModel } from 'src/app/Models/offer-owner.model';
import { OfferResponseModel } from 'src/app/Models/response-models/offer-response.model';
import { offersToExchangeSelector } from './store/home.selectors';

@Component({
  selector: 'offerDialog',
  templateUrl: 'offer-dialog.html',
})
export class OfferDialog {
  private _user: UserModel;
  user$ = this._store
    .pipe(select(userSelector))
    .subscribe((data) => (this._user = data));

  private _offerToExchange: OfferResponseModel[];
  offerToExchange$ = this._store
    .pipe(select(offersToExchangeSelector))
    .subscribe((data) => (this._offerToExchange = data));
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
    let photo: string;
    if (this.currentFile) {
      const reader = new FileReader();
      reader.readAsDataURL(this.currentFile.data);
      reader.onload = () => {
        photo = reader.result.toString();
      };
    }
    let offerOwnerModel = new OfferOwnerModel();

    let requestModel = new UploadOfferRequestModel();
    requestModel.categoryId = 1;
    requestModel.offerDescription = this.form['descriptor'].value;
    requestModel.offerOwnerDetail = offerOwnerModel;
    requestModel.offerPhoto = photo;
    requestModel.userEntity = this._user;
    requestModel.userId = this._user.id;

    //TODO create upload offer request model for send to server
    this._store.dispatch(
      HomeActions.UploadOfferForDiscussAction({ payload: requestModel })
    );
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
