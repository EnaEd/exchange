import { mergeMap, catchError, map } from 'rxjs/operators';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { DiscussService } from '../../../services/discuss.service';
import * as DiscussActions from './discuss.actions';
import { Injectable } from '@angular/core';
import * as BaseActions from './../../../store/app.actions';

@Injectable()
export class DiscussEffects {
  constructor(
    private _actions: Actions,
    private _discussService: DiscussService
  ) {}

  getChatRooms$ = createEffect(() =>
    this._actions.pipe(
      ofType(DiscussActions.GetChatRoomsAction),
      mergeMap((action) =>
        this._discussService.getRooms(action.client).pipe(
          map((data) => ({
            type: DiscussActions.IDiscussActionEnum.GetChatRoomsSuccess,
            rooms: data,
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
}
