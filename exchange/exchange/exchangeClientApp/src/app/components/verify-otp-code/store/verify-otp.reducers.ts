import { IVerifyOtpState, initialVerifyOtpState } from './verify-otp.state';
import { Action, createReducer, on } from '@ngrx/store';
import * as VerifyOPTActions from './verify-otp.actions';
import { state } from '@angular/animations';

const _reducer = createReducer(
  initialVerifyOtpState,
  on(VerifyOPTActions.SendVerifyOtpCodeSuccessAction, (state, { model }) => ({
    ...state,
    device: model.device,
    isVerified: true,
  }))
);

export function verifyOtpReducer(
  state: IVerifyOtpState | undefined,
  action: Action
): IVerifyOtpState {
  return _reducer(state, action);
}
