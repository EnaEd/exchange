import {
  CheckIsUserExistSuccessAction,
  SignInSuccessAction,
  SignUpSuccessAction,
  SignOutSuccessAction,
} from './auth.actions';
import { createReducer, on, Action } from '@ngrx/store';
import { initialAuthState, IAuthState } from './auth.state';

const reducer = createReducer(
  initialAuthState,
  on(CheckIsUserExistSuccessAction, (state, { payload }) => ({
    ...state,
    isUserExists: payload,
  })),
  on(SignInSuccessAction, (state, { payload }) => ({
    ...state,
    user: payload,
    isAuthenticate: true,
  })),
  on(SignUpSuccessAction, (state, { payload }) => ({
    ...state,
    user: payload,
    isAuthenticate: true,
  })),
  on(SignOutSuccessAction, (state) => ({
    ...state,
    user: initialAuthState.user,
    isAuthenticate: initialAuthState.isAuthenticate,
    isUserExists: initialAuthState.isUserExists,
  }))
);
export function authReducer(state: IAuthState | undefined, action: Action) {
  return reducer(state, action);
}
