import { OfferDialog } from './offer-dialog';
import { map } from 'rxjs/operators';
import { CategoryBottomSheet } from './category-bottom-sheet';
import {
  categorySelector,
  selectedCategorySelector,
  offersToExchangeSelector,
  fileUploadSelector,
} from './store/home.selectors';
import { GetCategoryAction } from './store/home.actions';
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

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
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
    // this._store.dispatch(GetCategoryAction());
    // this._bottomSheet.open(CategoryBottomSheet);
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    this.dialog.open(OfferDialog, dialogConfig);
  }

  LetDiscuss(index: number) {
    console.log(index);
    // const dialogConfig = new MatDialogConfig();

    // dialogConfig.disableClose = true;
    // dialogConfig.autoFocus = true;

    // this.dialog.open(OfferDialog, dialogConfig);
  }
}
