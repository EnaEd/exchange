import { IAppState } from './app.state';
import { createSelector } from '@ngrx/store';
import { IAuthState } from '../components/auth/store/auth.state';

export const auth = (state: IAppState) => state.auth;
export const isLogged = createSelector(
  auth,
  (state: IAuthState) => state.isAuthenticate
);
