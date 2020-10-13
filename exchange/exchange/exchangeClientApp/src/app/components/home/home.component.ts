import { map } from 'rxjs/operators';
import { CategoryBottomSheet } from './category-bottom-sheet';
import {
  categorySelector,
  selectedCategorySelector,
} from './store/home.selectors';
import { GetCategoryAction } from './store/home.actions';
import { IAppState } from 'src/app/store/app.state';
import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { MatBottomSheet } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  category$ = this._store.pipe(select(categorySelector));
  selectedCategory$ = this._store
    .pipe(select(selectedCategorySelector))
    .subscribe((data) => {});
  constructor(
    private _store: Store<IAppState>,
    private _bottomSheet: MatBottomSheet
  ) {}

  ngOnInit(): void {}

  GetCategory() {
    this._store.dispatch(GetCategoryAction());
    this._bottomSheet.open(CategoryBottomSheet);
  }
}
