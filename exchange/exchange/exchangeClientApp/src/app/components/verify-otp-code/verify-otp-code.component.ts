import { IAppState } from 'src/app/store/app.state';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-verify-otp-code',
  templateUrl: './verify-otp-code.component.html',
  styleUrls: ['./verify-otp-code.component.css'],
})
export class VerifyOTPCodeComponent implements OnInit {
  public otpCode: FormGroup = new FormGroup({
    code: new FormControl('', Validators.required),
  });

  constructor(
    private _toaster: ToastrService,
    private _store: Store<IAppState>
  ) {}

  ngOnInit(): void {}
  onSubmit(): void {
    //this._store.dispatch()
  }
}
