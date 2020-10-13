import { userSelector } from './../auth/store/auth.selectors';
import { UserModel } from 'src/app/Models/user.model';
import { OfferRequestModel } from './../../Models/RequestModels/offer-request.model';
import { CarteGoryExchangeResponseModel } from './../../Models/response-models/category-exchange-response.model';
import {
  categorySelector,
  selectedCategorySelector,
} from './store/home.selectors';
import { IAppState } from './../../store/app.state';
import { Store, select } from '@ngrx/store';
import { Component } from '@angular/core';
import { MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MatListModule } from '@angular/material/list';
import * as HomeActions from './store/home.actions';

@Component({
  selector: 'sheet',
  templateUrl: 'category-bottom-sheet.html',
})
export class CategoryBottomSheet {
  user: UserModel;
  user$ = this._store.pipe(select(userSelector));

  category$ = this._store.pipe(select(categorySelector));
  selectedCategory$ = this._store.pipe(select(selectedCategorySelector));
  constructor(
    private _bottomSheet: MatBottomSheetRef<CategoryBottomSheet>,
    private _store: Store<IAppState>
  ) {}

  SelectCategory(item: CarteGoryExchangeResponseModel) {
    this._store.dispatch(
      HomeActions.SetSelectedCategoryAction({ selectedCategory: item })
    );
    // this._bottomSheet.dismiss();
    // event.preventDefault();
  }
}
