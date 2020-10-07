import { IAppState } from 'src/app/store/app.state';
import { state } from '@angular/animations';
import { createSelector } from '@ngrx/store';
import { IVerifyOtpState } from './verify-otp.state';

const isVerified = (state: IAppState) => state.verifyOtp;

export const isVerifiedSelector = createSelector(
  isVerified,
  (state: IVerifyOtpState) => state.isVerified
);
