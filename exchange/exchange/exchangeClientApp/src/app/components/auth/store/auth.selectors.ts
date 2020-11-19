import { IAppState } from 'src/app/store/app.state';
import { createSelector } from '@ngrx/store';
import { IAuthState } from './auth.state';

const user = (state: IAppState) => state.auth;
const descriptionEvent = (state: IAppState) => state.auth;
const eventSuccess = (state: IAppState) => state.auth;
const isAuthenticate = (state: IAppState) => state.auth;
const isUserExists = (state: IAppState) => state.auth;
const error = (state: IAppState) => state.errors;
const authyId = (state: IAppState) => state.auth;
const accessToken = (state: IAppState) => state.auth;

export const tokenSelector = createSelector(
  accessToken,
  (state) => state.accessToken
);

export const isUserExistsSelector = createSelector(
  isUserExists,
  (state: IAuthState) => state.isUserExists
);
export const isAuthenticateSelector = createSelector(
  isAuthenticate,
  (state: IAuthState) => state.isAuthenticate
);
export const userSelector = createSelector(
  user,
  (state: IAuthState) => state.user
);
export const descriptionEventSelector = createSelector(
  descriptionEvent,
  (state: IAuthState) => state.descriptionEvent
);
export const eventSuccessSelector = createSelector(
  eventSuccess,
  (state: IAuthState) => state.eventSuccess
);
export const authyIdSelector = createSelector(
  authyId,
  (state: IAuthState) => state.authyId
);
