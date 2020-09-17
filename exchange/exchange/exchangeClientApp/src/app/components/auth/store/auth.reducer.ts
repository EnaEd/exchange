import {
  CheckIsUserExistSuccessAction,
  SignInSuccessAction,
  SignUpSuccessAction,
  SignOutSuccessAction,
  RequestsSMSCodeSuccessfullAction,
  SignInErrorAction,
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
    descriptionEvent: payload,
    eventSuccess: true,
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
  })),
  on(RequestsSMSCodeSuccessfullAction, (state, { payload }) => ({
    ...state,
    descriptionEvent: payload,
  })),
  on(SignInErrorAction, (state, { payload }) => ({
    ...state,
    user: payload,
  }))
);
export function authReducer(state: IAuthState | undefined, action: Action) {
  return reducer(state, action);
}
