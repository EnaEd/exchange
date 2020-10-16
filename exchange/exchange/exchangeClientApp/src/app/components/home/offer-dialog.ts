import { FileUploadModel } from './../../Models/file-upload.model';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  HttpRequest,
  HttpClient,
  HttpEventType,
  HttpErrorResponse,
} from '@angular/common/http';
import { tap, last, catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';

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
  private file: FileUploadModel;
  constructor(
    private _dialogRef: MatDialogRef<OfferDialog>,
    private _http: HttpClient
  ) {}
  public form: FormGroup = new FormGroup({
    description: new FormControl(''),
  });

  Close() {
    this._dialogRef.close();
  }
  Save() {}

  Upload() {
    const fileUpload = document.getElementById(
      'fileUpload'
    ) as HTMLInputElement;
    fileUpload.onchange = () => {
      for (let index = 0; index < fileUpload.files.length; index++) {
        const file = fileUpload.files[index];
        this.file = {
          data: file,
          state: 'in',
          inProgress: false,
          progress: 0,
          canRetry: false,
          canCancel: true,
        };
      }
      //   this.uploadFile(this.file);
    };
    fileUpload.click();
  }

  private uploadFile(file: FileUploadModel) {
    const fd = new FormData();
    fd.append(this.param, file.data);

    const req = new HttpRequest('POST', this.target, fd, {
      reportProgress: true,
    });

    file.inProgress = true;
    file.sub = this._http
      .request(req)
      .pipe(
        map((event) => {
          switch (event.type) {
            case HttpEventType.UploadProgress:
              file.progress = Math.round((event.loaded * 100) / event.total);
              break;
            case HttpEventType.Response:
              return event;
          }
        }),
        tap((message) => {}),
        last(),
        catchError((error: HttpErrorResponse) => {
          file.inProgress = false;
          file.canRetry = true;
          return of(`${file.data.name} upload failed.`);
        })
      )
      .subscribe((event: any) => {
        console.log(event);

        // if (typeof event === 'object') {
        //   //this.removeFileFromArray(file);
        //   //this.complete.emit(event.body);
        // }
      });
  }
  private removeFileFromArray(file: FileUploadModel) {
    this.file = null;
  }
}
