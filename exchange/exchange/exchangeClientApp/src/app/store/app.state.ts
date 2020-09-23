import {
  IAuthState,
  initialAuthState,
} from './../components/auth/store/auth.state';
import { RouterReducerState } from '@ngrx/router-store';

export interface IAppState {
  [x: string]: any;
  router?: RouterReducerState;
  auth: IAuthState;
}
export const initialAppState: IAppState = {
  auth: initialAuthState,
};
export function getInitialAppState(): IAppState {
  return initialAppState;
}
