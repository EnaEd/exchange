import { state } from '@angular/animations';
import { BaseErrorAction, BaseClearErrorAction } from './app.actions';
import {
  ActionReducerMap,
  createReducer,
  on,
  Action,
  props,
} from '@ngrx/store';
import { IAppState, initialBaseState, IBaseState } from './app.state';
import { routerReducer } from '@ngrx/router-store';
import { authReducer } from '../components/auth/store/auth.reducer';
import { verifyOtpReducer } from '../components/verify-otp-code/store/verify-otp.reducers';

const _reducer = createReducer(
  initialBaseState,
  on(BaseErrorAction, (state, { errors }) => ({
    ...state,
    errors: errors,
  })),
  on(BaseClearErrorAction, (state) => ({
    ...state,
    errors: [],
  }))
);

export function baseReducer(
  state: IBaseState | undefined,
  action: Action
): IBaseState {
  return _reducer(state, action);
}

export const appReducers: ActionReducerMap<IAppState, any> = {
  router: routerReducer,
  auth: authReducer,
  verifyOtp: verifyOtpReducer,
  base: baseReducer,
};
