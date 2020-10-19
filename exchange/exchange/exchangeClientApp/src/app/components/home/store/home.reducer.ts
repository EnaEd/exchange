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
  ),
  on(HomeActions.SetSelectedPlaceAction, (state, { place }) => ({
    ...state,
    selectedPlace: place,
  })),
  on(HomeActions.GetFileForUploadAction, (state, { payload }) => ({
    ...state,
    fileForUpload: payload,
  })),
  on(HomeActions.ClearFileForUploadAction, (state) => ({
    ...state,
    fileForUpload: null,
  }))
);

export function homeReducer(state: IHomeState | undefined, action: Action) {
  return reducer(state, action);
}
