import { AuthService } from './../../../services/auth.service';
import { mergeMap } from 'rxjs/operators';
import { createEffect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { Action } from '@ngrx/store';
import * as VerifyActions from './verify-otp.actions';

@Injectable()
export class VerifyOtpEffect {
  //   sendOtpCodeToVerify$ = createEffect(() => {
  //       this._action.pipe(
  //           ofType(VerifyActions.VerifyOptCodeActionEnum.SendVerifyOtpCode),
  //           mergeMap(action=>
  //             )
  //       )
  //   });

  constructor(private _action: Action, private _authService: AuthService) {}
}
