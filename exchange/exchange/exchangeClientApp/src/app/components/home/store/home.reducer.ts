import { state } from '@angular/animations';
import { initialHomeState, IHomeState } from './home.state';
import { createReducer, Action, on } from '@ngrx/store';
import * as HomeActions from './home.actions';

const reducer = createReducer(
  initialHomeState,
  on(HomeActions.GetCategorySuccessAction, (state, { payload }) => ({
    ...state,
    category: payload,
  })),
  on(HomeActions.SetSelectedCategoryAction, (state, { selectedCategory }) => ({
    ...state,
    selectedCategory: selectedCategory,
  })),
  on(
    HomeActions.GetOfferByCategorySuccessAction,
    (state, { offersForExchange }) => ({
      ...state,
      offersToExchange: offersForExchange,
    })
  )
);

export function homeReducer(state: IHomeState | undefined, action: Action) {
  return reducer(state, action);
}
