import { state } from '@angular/animations';
import { IAppState } from './app.state';
import { createSelector } from '@ngrx/store';
import { IAuthState } from '../components/auth/store/auth.state';

export const auth = (state: IAppState) => state.auth;
export const error = (state: IAppState) => state;

export const isLoggedSelector = createSelector(
  auth,
  (state: IAuthState) => state.isAuthenticate
);
export const erorrsSelector = createSelector(
  error,
  (state: IAppState) => state.base.errors
);
