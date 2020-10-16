import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'offerDialog',
  templateUrl: 'offer-dialog.html',
})
export class OfferDialog {
  constructor(private _dialogRef: MatDialogRef<OfferDialog>) {}
  public form: FormGroup = new FormGroup({
    description: new FormControl(''),
  });

  Close() {
    this._dialogRef.close();
  }
  Save() {}
}
