import { AuthService } from './../../services/auth.service';
import * as AuthActions from './auth.actions';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { empty } from 'rxjs';

@Injectable()
export class AuthEffects {
  signInEffect$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.SignInAction),
      mergeMap((action) =>
        this._authService.signIn(action.model).pipe(
          map((data) => {
            debugger;
            console.log(data);
            return {
              type: AuthActions.AuthActionEnum.SignInSuccess,
              payload: data,
            };
          }),
          catchError(() => {
            return empty;
          })
        )
      )
    )
  );

  constructor(private actions$: Actions, private _authService: AuthService) {}
}
