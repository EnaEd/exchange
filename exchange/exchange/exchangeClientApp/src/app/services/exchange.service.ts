import { OfferRequestModel } from './../Models/RequestModels/offer-request.model';
import { OfferResponseModel } from './../Models/response-models/offer-response.model';
import { Observable } from 'rxjs';
import { CarteGoryExchangeResponseModel } from './../Models/response-models/category-exchange-response.model';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';

@Injectable()
export class ExchangeService {
  constructor(private _httpService: HttpService) {}
  getOfferCategories(): Observable<CarteGoryExchangeResponseModel[]> {
    return this._httpService.get(
      `${environment.apiURL}/Exchange/getoffercategories`
    ) as Observable<CarteGoryExchangeResponseModel[]>;
  }
  showOffer(model: OfferRequestModel): Observable<OfferResponseModel[]> {
    return this._httpService.post(
      `${environment.apiURL}/Exchange/showoffer`,
      model
    ) as Observable<OfferResponseModel[]>;
  }
  uploadOffer(model: any): any {}
}
