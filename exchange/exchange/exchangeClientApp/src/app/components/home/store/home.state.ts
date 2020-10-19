import { FileUploadModel } from './../../../Models/file-upload.model';
import { PlaceModel } from './../../../Models/place.model';
import { OfferResponseModel } from './../../../Models/response-models/offer-response.model';
import { CarteGoryExchangeResponseModel } from './../../../Models/response-models/category-exchange-response.model';
export interface IHomeState {
  category: CarteGoryExchangeResponseModel[];
  selectedCategory: CarteGoryExchangeResponseModel;
  offersToExchange: OfferResponseModel[];
  selectedPlace: PlaceModel;
  fileForUpload: FileUploadModel;
}
export const initialHomeState: IHomeState = {
  category: null,
  selectedCategory: null,
  offersToExchange: null,
  selectedPlace: null,
  fileForUpload: null,
};
