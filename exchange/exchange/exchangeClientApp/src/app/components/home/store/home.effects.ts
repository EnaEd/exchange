import { ToastrService } from 'ngx-toastr';
import { DiscussService } from './../../../services/discuss.service';
import * as BaseActions from './../../../store/app.actions';
import * as HomeActions from './home.actions';
import { mergeMap, catchError, map } from 'rxjs/operators';
import { ExchangeService } from './../../../services/exchange.service';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import * as DiscussActions from '../../discuss/store/discuss.actions';

@Injectable()
export class HomeEffects {
  constructor(
    private _actions: Actions,
    private _exchangeService: ExchangeService,
    private _discussService: DiscussService,
    private _toaster: ToastrService
  ) {}

  createDiscuss$ = createEffect(() =>
    this._actions.pipe(
      ofType(HomeActions.CreateDiscuss),
      mergeMap((action) =>
        this._discussService.createChatRoom(action.model).pipe(
          map(
            (data) => (
              this._toaster.success('discuss was created'),
              {
                type: HomeActions.HomeActionEnum.CreateDiscussSuccess,
                model: data,
              }
            )
          ),
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

  getOffersByCategory$ = createEffect(() =>
    this._actions.pipe(
      ofType(HomeActions.GetOfferByCategoryAction),
      mergeMap((action) =>
        this._exchangeService.showOffer(action.requestForOffer).pipe(
          map((data) => ({
            type: HomeActions.HomeActionEnum.GetOfferByCategorySuccess,
            offersForExchange: data,
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

  getCategoryEffect$ = createEffect(() =>
    this._actions.pipe(
      ofType(HomeActions.GetCategoryAction),
      mergeMap((action) =>
        this._exchangeService.getOfferCategories().pipe(
          map((data) => ({
            type: HomeActions.HomeActionEnum.GetCategorySuccess,
            payload: data,
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
