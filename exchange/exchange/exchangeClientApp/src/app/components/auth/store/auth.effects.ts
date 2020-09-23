import { BaseModel } from './../../../Models/base.model';
import { UserModel } from './../../../Models/user.model';
import { AuthService } from './../../services/auth.service';
import * as AuthActions from './auth.actions';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { empty } from 'rxjs';
import { JsonPipe } from '@angular/common';

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
          })),
          catchError(async (data) => {
            let parseUser: UserModel = {
              id: JSON.parse(`"${data.error.Id}"`),
              errors: JSON.parse(`"${data.error.Errors}"`),
            } as UserModel;
            return {
              type: AuthActions.AuthActionEnum.SignInError,
              payload: parseUser,
            };
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private _authService: AuthService) {}
}
