import { OfferRequestModel } from './../../../Models/RequestModels/offer-request.model';
import { OfferResponseModel } from './../../../Models/response-models/offer-response.model';
import { CarteGoryExchangeResponseModel } from './../../../Models/response-models/category-exchange-response.model';
import { createAction, props } from '@ngrx/store';
export enum HomeActionEnum {
  GetCategory = '[Home action] Get category',
  GetCategorySuccess = '[Home action] Get category success',
  GetCategoryError = '[Home action] Get category error',
  SetSelectedCategory = '[Home action] Set selected category',
  GetOfferByCategory = '[Home action] get offer by category',
  GetOfferByCategorySuccess = '[Home action] get offer by category success',
  GetOfferByCategoryError = '[Home action] get offer by category errror',
}

export const GetOfferByCategorySuccessAction = createAction(
  HomeActionEnum.GetOfferByCategorySuccess,
  props<{ offersForExchange: OfferResponseModel[] }>()
);
export const GetOfferByCategoryAction = createAction(
  HomeActionEnum.GetOfferByCategory,
  props<{ requestForOffer: OfferRequestModel }>()
);
export const GetCategoryAction = createAction(HomeActionEnum.GetCategory);
export const GetCategorySuccessAction = createAction(
  HomeActionEnum.GetCategorySuccess,
  props<{ payload: CarteGoryExchangeResponseModel[] }>()
);
export const SetSelectedCategoryAction = createAction(
  HomeActionEnum.SetSelectedCategory,
  props<{ selectedCategory: CarteGoryExchangeResponseModel }>()
);
