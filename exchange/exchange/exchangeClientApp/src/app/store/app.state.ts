import {
  IVerifyOtpState,
  initialVerifyOtpState,
} from './../components/verify-otp-code/store/verify-otp.state';
import {
  IAuthState,
  initialAuthState,
} from './../components/auth/store/auth.state';
import { RouterReducerState } from '@ngrx/router-store';

export interface IAppState {
  [x: string]: any;
  router?: RouterReducerState;
  auth: IAuthState;
  verifyOtp: IVerifyOtpState;
}
export const initialAppState: IAppState = {
  auth: initialAuthState,
  verifyOtp: initialVerifyOtpState,
};
export function getInitialAppState(): IAppState {
  return initialAppState;
}
