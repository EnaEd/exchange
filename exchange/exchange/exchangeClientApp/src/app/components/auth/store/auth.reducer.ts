import {
  CheckIsUserExistSuccessAction,
  SignInSuccessAction,
  SignUpSuccessAction,
  SignOutSuccessAction,
  RequestsSMSCodeSuccessfullAction,
  SignInErrorAction,
  LoggedSuccessAction,
} from './auth.actions';
import { createReducer, on, Action } from '@ngrx/store';
import { initialAuthState, IAuthState } from './auth.state';

const reducer = createReducer(
  initialAuthState,
  on(CheckIsUserExistSuccessAction, (state, { payload }) => ({
    ...state,
    isUserExists: payload,
  })),
  on(SignInSuccessAction, (state, { payload, authyId }) => ({
    ...state,
    descriptionEvent: payload,
    eventSuccess: true,
    authyId: Number.parseInt(authyId),
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
  on(SignInErrorAction, (state, { errors }) => ({
    ...state,
    errors: errors,
  })),
  on(LoggedSuccessAction, (state) => ({
    ...state,
    isAuthenticate: true,
  }))
);
export function authReducer(state: IAuthState | undefined, action: Action) {
  return reducer(state, action);
}
