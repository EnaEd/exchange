import { map } from 'rxjs/operators';
import { PlaceModel } from './../../Models/place.model';
import { userSelector } from './../auth/store/auth.selectors';
import { UserModel } from 'src/app/Models/user.model';
import { OfferRequestModel } from './../../Models/RequestModels/offer-request.model';
import { CarteGoryExchangeResponseModel } from './../../Models/response-models/category-exchange-response.model';
import {
  categorySelector,
  selectedCategorySelector,
  selectedPlaceSelector,
} from './store/home.selectors';
import { IAppState } from './../../store/app.state';
import { Store, select } from '@ngrx/store';
import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MatListModule } from '@angular/material/list';
import * as HomeActions from './store/home.actions';
import PlaceResult = google.maps.places.PlaceResult;
import {
  Appearance,
  Location,
} from '@angular-material-extensions/google-maps-autocomplete';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'sheet',
  templateUrl: 'category-bottom-sheet.html',
  encapsulation: ViewEncapsulation.None,
})
export class CategoryBottomSheet implements OnInit {
  public user: UserModel;
  public appearance = Appearance;
  public zoom: number;
  public latitude: number;
  public longitude: number;
  public selectedAddress: PlaceResult;
  public selectedCategory: CarteGoryExchangeResponseModel;
  user$ = this._store.pipe(select(userSelector));

  category$ = this._store.pipe(select(categorySelector));
  selectedCategory$ = this._store.pipe(select(selectedCategorySelector));

  place$ = this._store.pipe(select(selectedPlaceSelector)).subscribe((data) => {
    if (data) {
      let model = new OfferRequestModel();
      this.selectedCategory$.subscribe((data) => (model.categoryId = data.id));
      model.city = data.city;
      model.country = data.country;
      this._store.dispatch(
        HomeActions.GetOfferByCategoryAction({ requestForOffer: model })
      );
    }
  });

  constructor(
    private _titleService: Title,
    private _bottomSheet: MatBottomSheetRef<CategoryBottomSheet>,
    private _store: Store<IAppState>
  ) {}
  ngOnInit(): void {
    this._titleService.setTitle(
      'Home | @angular-material-extensions/google-maps-autocomplete'
    );

    this.zoom = 10;
    this.latitude = 52.520008;
    this.longitude = 13.404954;

    this.setCurrentPosition();
  }

  private setCurrentPosition() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        this.latitude = position.coords.latitude;
        this.longitude = position.coords.longitude;
        this.zoom = 12;
      });
    }
  }

  onAutocompleteSelected(result: PlaceResult) {
    console.log('onAutocompleteSelected: ', result);
    let place = new PlaceModel();
    place.city = result.address_components.find((item) =>
      item.types.find((type) => type == 'locality')
    ).long_name;
    place.country = result.address_components.find((item) =>
      item.types.find((type) => type == 'country')
    ).long_name;
    this._store.dispatch(HomeActions.SetSelectedPlaceAction({ place }));
    this._bottomSheet.dismiss();
  }

  onLocationSelected(location: Location) {
    console.log('onLocationSelected: ', location);
    this.latitude = location.latitude;
    this.longitude = location.longitude;
  }

  SelectCategory(item: CarteGoryExchangeResponseModel) {
    this._store.dispatch(
      HomeActions.SetSelectedCategoryAction({ selectedCategory: item })
    );
  }
}
