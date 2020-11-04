import { ChatRequestModel } from './../../../Models/RequestModels/chat-request.model';
import { UploadOfferRequestModel } from './../../../Models/RequestModels/upload-offer-request.model';
import { FileUploadModel } from './../../../Models/file-upload.model';
import { PlaceModel } from './../../../Models/place.model';
import { OfferRequestModel } from './../../../Models/RequestModels/offer-request.model';
import { OfferResponseModel } from './../../../Models/response-models/offer-response.model';
import { CarteGoryExchangeResponseModel } from './../../../Models/response-models/category-exchange-response.model';
import { createAction, props } from '@ngrx/store';
import { ChatModel } from 'src/app/Models/chat.model';
export enum HomeActionEnum {
  GetCategory = '[Home action] Get category',
  GetCategorySuccess = '[Home action] Get category success',
  GetCategoryError = '[Home action] Get category error',
  SetSelectedCategory = '[Home action] Set selected category',
  GetOfferByCategory = '[Home action] get offer by category',
  GetOfferByCategorySuccess = '[Home action] get offer by category success',
  GetOfferByCategoryError = '[Home action] get offer by category errror',
  SetSelectedPlace = '[Home action] set selected place',
  GetFileForUpload = '[Hom action] get file for upload',
  ClearFileForUpload = '[Home action] clear file for upload',
  UploadOfferForDiscuss = '[Home action] upload offer for discuss',
  UploadOfferForDiscussSuccess = '[Home action] upload offer for discuss success',
  SetSelectedOfferToExchange = '[Home action] set selected offer to exchange',
  CreateDiscuss = '[Home action] create chat room',
  CreateDiscussSuccess = '[Home action] create chat room success',
}

export const CreateDiscussSuccess = createAction(
  HomeActionEnum.CreateDiscussSuccess,
  props<{ model: ChatModel }>()
);
export const CreateDiscuss = createAction(
  HomeActionEnum.CreateDiscuss,
  props<{ model: ChatRequestModel }>()
);
export const SetSelectedOfferToExchangeAction = createAction(
  HomeActionEnum.SetSelectedOfferToExchange,
  props<{ selectedOffer: OfferResponseModel }>()
);
export const UploadOfferForDiscussSuccessAction = createAction(
  HomeActionEnum.UploadOfferForDiscussSuccess,
  props<{ payload: string }>()
);
export const UploadOfferForDiscussAction = createAction(
  HomeActionEnum.UploadOfferForDiscuss,
  props<{ payload: UploadOfferRequestModel }>()
);
export const ClearFileForUploadAction = createAction(
  HomeActionEnum.ClearFileForUpload
);
export const GetFileForUploadAction = createAction(
  HomeActionEnum.GetFileForUpload,
  props<{ payload: FileUploadModel }>()
);
export const SetSelectedPlaceAction = createAction(
  HomeActionEnum.SetSelectedPlace,
  props<{ place: PlaceModel }>()
);
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
