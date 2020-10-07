import { VerifyOtpCodeResponseModel } from './../../../Models/response-models/verify-otp-code-response.model';
import { UserModel } from './../../../Models/user.model';
import { createAction, props } from '@ngrx/store';
import { VerifyOtpCodeRequestModel } from '../../../Models/RequestModels/verify-otp-code-request.model';
export enum VerifyOptCodeActionEnum {
  SendVerifyOtpCode = '[Verify OTP] send verify code to check',
  SendVerifyOtpCodeSuccess = '[Verify OTP] sending code success',
  SendVerifyOtpCodeError = '[Verify OTP] sending code error',
}
export const SendVerifyOtpCodeAction = createAction(
  VerifyOptCodeActionEnum.SendVerifyOtpCode,
  props<{ model: VerifyOtpCodeRequestModel }>()
);
export const SendVerifyOtpCodeSuccessAction = createAction(
  VerifyOptCodeActionEnum.SendVerifyOtpCodeSuccess,
  props<{ model: VerifyOtpCodeResponseModel }>()
);
export const SendVerifyOtpCodeErrorAction = createAction(
  VerifyOptCodeActionEnum.SendVerifyOtpCodeError,
  props<{ errors: string[] }>()
);
