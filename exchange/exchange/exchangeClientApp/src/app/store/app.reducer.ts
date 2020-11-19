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
import { homeReducer } from '../components/home/store/home.reducer';
import { discussReducer } from '../components/discuss/store/discuss.reducer';
import { authReducer } from '../components/auth/store/auth.reducer';
import { verifyOtpReducer } from '../components/verify-otp-code/store/verify-otp.reducers';
import { AuthActionEnum } from '../components/auth/store/auth.actions';
import { uploadReducer } from '../components/upload-offer/store/upload-offer.reducer';

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
export function clearReduser(reducer) {
  return function (state: IAppState, action: Action) {
    if (action.type == AuthActionEnum.SignOut) {
      state = undefined;
    }
    return reducer(state, action);
  };
}
export const appReducers: ActionReducerMap<IAppState, any> = {
  router: routerReducer,
  auth: authReducer,
  verifyOtp: verifyOtpReducer,
  base: baseReducer,
  home: homeReducer,
  discuss: discussReducer,
  uploadOfffer: uploadReducer,
};
