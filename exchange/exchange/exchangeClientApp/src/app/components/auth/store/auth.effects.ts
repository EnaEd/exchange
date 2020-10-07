import { BaseModel } from './../../../Models/base.model';
import { UserModel } from './../../../Models/user.model';
import { AuthService } from './../../../services/auth.service';
import * as AuthActions from './auth.actions';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { empty } from 'rxjs';
import { JsonPipe } from '@angular/common';
import * as BaseActions from '../../../store/app.actions';

@Injectable()
export class AuthEffects {
  signInEffect$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SignInAction),
      mergeMap((action) =>
        this._authService.signIn(action.model).pipe(
          map((data) => ({
            type: AuthActions.AuthActionEnum.SignInSuccess,
            payload: data.message,
            authyId: data.authyId,
          })),
          catchError(async (data) => {
            let errors: string[] = JSON.parse(`"${data.error.Errors}"`);
            return {
              type: BaseActions.BaseActionEnum.ErrorActionEnum,
              errors: errors,
            };
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private _authService: AuthService) {}
}
