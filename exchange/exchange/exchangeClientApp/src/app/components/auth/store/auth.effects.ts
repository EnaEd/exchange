import { AuthService } from './../../services/auth.service';
import * as AuthActions from './auth.actions';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { empty } from 'rxjs';

@Injectable()
export class AuthEffects {
  createAuthyUserEffect$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SendSMSCodeAction),
      mergeMap((action) =>
        this._authService
          .createAuthyUser({
            countryCode: action.authyModel.countryCode,
            email: action.authyModel.email,
            phone: action.authyModel.phone,
          })
          .pipe(
            map((data) => ({
              type: AuthActions.AuthActionEnum.SendSMSCodeSuccess,
              payload: data.userId,
            })),
            catchError(() => empty)
          )
      )
    )
  );
  sendSmsCodeEffect$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SendSMSCodeSuccess),
      mergeMap((action) =>
        this._authService.sendrequestSMSCode(action.userId).pipe(
          map((data) => ({
            type: AuthActions.AuthActionEnum.RequestsSMSCodeSuccessfull,
            payload: data,
          }))
        )
      )
    )
  );

  constructor(private actions$: Actions, private _authService: AuthService) {}
}
