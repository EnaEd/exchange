import { PlaceModel } from './../../../Models/place.model';
import { OfferResponseModel } from './../../../Models/response-models/offer-response.model';
import { CarteGoryExchangeResponseModel } from './../../../Models/response-models/category-exchange-response.model';
export interface IHomeState {
  category: CarteGoryExchangeResponseModel[];
  selectedCategory: CarteGoryExchangeResponseModel;
  offersToExchange: OfferResponseModel[];
  selectedPlace: PlaceModel;
}
export const initialHomeState: IHomeState = {
  category: null,
  selectedCategory: null,
  offersToExchange: null,
  selectedPlace: null,
};
