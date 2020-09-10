import { ActionReducerMap } from '@ngrx/store';
import { IAppState } from './app.state';
import { routerReducer } from '@ngrx/router-store';
import { authReducer } from '../components/auth/store/auth.reducer';

export const appReducers: ActionReducerMap<IAppState, any> = {
  router: routerReducer,
  auth: authReducer,
};
