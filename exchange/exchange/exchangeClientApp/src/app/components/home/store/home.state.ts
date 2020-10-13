import { OfferResponseModel } from './../../../Models/response-models/offer-response.model';
import { CarteGoryExchangeResponseModel } from './../../../Models/response-models/category-exchange-response.model';
export interface IHomeState {
  category: CarteGoryExchangeResponseModel[];
  selectedCategory: CarteGoryExchangeResponseModel;
  offersToExchange: OfferResponseModel[];
}
export const initialHomeState: IHomeState = {
  category: null,
  selectedCategory: null,
  offersToExchange: null,
};
