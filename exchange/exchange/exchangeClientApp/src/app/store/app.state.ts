import {
  IHomeState,
  initialHomeState,
} from './../components/home/store/home.state';
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
  home: IHomeState;
}
export const initialAppState: IAppState = {
  auth: initialAuthState,
  verifyOtp: initialVerifyOtpState,
  base: initialBaseState,
  home: initialHomeState,
};
export function getInitialAppState(): IAppState {
  return initialAppState;
}
