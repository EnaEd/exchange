import { AuthService } from './../../../services/auth.service';
import { mergeMap, map, catchError, concatMap } from 'rxjs/operators';
import { createEffect, ofType, Actions } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import * as VerifyActions from './verify-otp.actions';
import * as BaseActions from '../../../store/app.actions';
import * as AuthActions from '../../auth/store/auth.actions';
import { of } from 'rxjs';

@Injectable()
export class VerifyOtpEffect {
  // sendOtpCodeToVerify$ = createEffect(() =>
  //   this._action$.pipe(
  //     ofType(VerifyActions.SendVerifyOtpCodeAction),
  //     mergeMap((action) =>
  //       this._authService.verifyOTPCode(action.model).pipe(
  //         map((data) => ({
  //           type:
  //             VerifyActions.VerifyOptCodeActionEnum.SendVerifyOtpCodeSuccess,
  //           model: data,
  //         })),
  //         catchError(async (data) => {
  //           debugger;
  //           let errors: string[] = data.error.Errors;
  //           return {
  //             type: BaseActions.BaseActionEnum.ErrorActionEnum,
  //             errors: data.error.Errors,
  //           };
  //         })
  //       )
  //     )
  //   )
  // );

  sendOtpCodeToVerify$ = createEffect(() =>
    this._action$.pipe(
      ofType(VerifyActions.SendVerifyOtpCodeAction),
      mergeMap((action) =>
        this._authService.verifyOTPCode(action.model).pipe(
          concatMap((data) =>
            of(
              { type: AuthActions.AuthActionEnum.LoggedSuccess },
              {
                type:
                  VerifyActions.VerifyOptCodeActionEnum
                    .SendVerifyOtpCodeSuccess,
                model: data,
              },
              {
                type: AuthActions.AuthActionEnum.SignInSuccessAndVerifed,
                user: data.user,
              },
              {
                type: AuthActions.AuthActionEnum.SignInGenerateTokenSuccess,
                token: data.accessToken,
              }
            )
          ),
          catchError(async (data) => {
            debugger;
            let errors: string[] = data.error.Errors;
            return {
              type: BaseActions.BaseActionEnum.ErrorActionEnum,
              errors: data.error.Errors,
            };
          })
        )
      )
    )
  );

  constructor(private _action$: Actions, private _authService: AuthService) {}
}
