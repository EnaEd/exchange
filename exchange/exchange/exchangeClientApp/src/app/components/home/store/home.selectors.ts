import { state } from '@angular/animations';
import { IHomeState } from './home.state';
import { createSelector } from '@ngrx/store';
import { IAppState } from '../../../../app/store/app.state';

const category = (state: IAppState) => state.home;
const selectedCategory = (state: IAppState) => state.home;
const place = (state: IAppState) => state.home;
const offers = (state: IAppState) => state.home;

export const offersToExchangeSelector = createSelector(
  offers,
  (state) => state.offersToExchange
);
export const selectedPlaceSelector = createSelector(
  place,
  (state) => state.selectedPlace
);
export const categorySelector = createSelector(
  category,
  (state) => state.category
);
export const selectedCategorySelector = createSelector(
  selectedCategory,
  (state: IHomeState) => state.selectedCategory
);
