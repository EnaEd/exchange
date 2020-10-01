import { IVerifyOtpState, initialVerifyOtpState } from './verify-otp.state';
import { Action, createReducer } from '@ngrx/store';

const _reducer = createReducer(initialVerifyOtpState);

export function verifyOtpReducer(
  state: IVerifyOtpState | undefined,
  action: Action
) {
  return _reducer(state, action);
}
