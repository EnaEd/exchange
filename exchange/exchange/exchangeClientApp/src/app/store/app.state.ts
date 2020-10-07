import {
  IVerifyOtpState,
  initialVerifyOtpState,
} from './../components/verify-otp-code/store/verify-otp.state';
import {
  IAuthState,
  initialAuthState,
} from './../components/auth/store/auth.state';
import { RouterReducerState } from '@ngrx/router-store';

export interface IBaseState {
  errors: string[];
}
export const initialBaseState = {
  errors: [],
};
export function getInitialBaseState(): IBaseState {
  return initialBaseState;
}

export interface IAppState {
  [x: string]: any;
  router?: RouterReducerState;
  auth: IAuthState;
  verifyOtp: IVerifyOtpState;
  base: IBaseState;
}
export const initialAppState: IAppState = {
  auth: initialAuthState,
  verifyOtp: initialVerifyOtpState,
  base: initialBaseState,
};
export function getInitialAppState(): IAppState {
  return initialAppState;
}
