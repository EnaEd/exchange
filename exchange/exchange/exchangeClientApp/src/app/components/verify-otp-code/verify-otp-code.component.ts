import { authyIdSelector } from './../auth/store/auth.selectors';
import { IAppState } from 'src/app/store/app.state';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Store, select } from '@ngrx/store';
import * as VerifyActions from './store/verify-otp.actions';
import { VerifyOtpCodeRequestModel } from 'src/app/Models/RequestModels/verify-otp-code-request.model';

@Component({
  selector: 'app-verify-otp-code',
  templateUrl: './verify-otp-code.component.html',
  styleUrls: ['./verify-otp-code.component.css'],
})
export class VerifyOTPCodeComponent implements OnInit {
  public otpCode: FormGroup = new FormGroup({
    code: new FormControl('', Validators.required),
  });
  private _authyId: number;
  authyId$ = this._store.pipe(select(authyIdSelector)).subscribe((data) => {
    this._authyId = data;
  });

  constructor(
    private _toaster: ToastrService,
    private _store: Store<IAppState>
  ) {}

  ngOnInit(): void {}
  onSubmit(): void {
    let model = new VerifyOtpCodeRequestModel();
    model.authyId = this._authyId;
    model.token = this.otpCode.get('code').value;
    this._store.dispatch(VerifyActions.SendVerifyOtpCodeAction({ model }));
  }
}
