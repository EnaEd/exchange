import { userSelector } from './../auth/store/auth.selectors';
import { UserModel } from './../../Models/user.model';
import { OfferResponseModel } from './../../Models/response-models/offer-response.model';
import { OfferDialog } from './offer-dialog';
import { map, concatMap } from 'rxjs/operators';
import { CategoryBottomSheet } from './category-bottom-sheet';
import {
  categorySelector,
  selectedCategorySelector,
  offersToExchangeSelector,
  fileUploadSelector,
} from './store/home.selectors';
import * as HomeActions from './store/home.actions';
import { IAppState } from 'src/app/store/app.state';
import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import {
  MatDialog,
  MatDialogModule,
  MAT_DIALOG_DATA,
  MatDialogConfig,
} from '@angular/material/dialog';
import { ChatRequestModel } from 'src/app/Models/RequestModels/chat-request.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  private _user: UserModel;
  user$ = this._store
    .pipe(select(userSelector))
    .subscribe((data) => (this._user = data));
  fileForUpload$ = this._store.pipe(select(fileUploadSelector));
  offers$ = this._store.pipe(select(offersToExchangeSelector));
  category$ = this._store.pipe(select(categorySelector));
  selectedCategory$ = this._store
    .pipe(select(selectedCategorySelector))
    .subscribe((data) => {});
  constructor(
    private dialog: MatDialog,
    private _store: Store<IAppState>,
    private _bottomSheet: MatBottomSheet
  ) {}

  ngOnInit(): void {}

  GetCategory() {
    this._store.dispatch(HomeActions.GetCategoryAction());
    this._bottomSheet.open(CategoryBottomSheet);
  }

  LetDiscuss(index: number) {
    let offer: OfferResponseModel;
    this.offers$.subscribe((data) => (offer = data[index]));
    this._store.dispatch(
      HomeActions.SetSelectedOfferToExchangeAction({ selectedOffer: offer })
    );
    let requestModel = new ChatRequestModel();
    debugger;
    requestModel.chatName =
      offer.description.length > 10
        ? `${offer.description.substring(0, 10)}...`
        : `${offer.description}`;
    //TODO EE:delete this after test
    requestModel.createrId = 22;
    requestModel.prticipantIds = [22, 2];
    //TODO add this data after test
    // requestModel.createrId = this._user.id;
    //requestModel.prticipantIds = [this._user.id, offer.user.id];
    this._store.dispatch(HomeActions.CreateDiscuss({ model: requestModel }));
  }
}
