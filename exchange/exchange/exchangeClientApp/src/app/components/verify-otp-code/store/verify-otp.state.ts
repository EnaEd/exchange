import { AuthyDeviceModel } from './../../../Models/authy-device.model';
export interface IVerifyOtpState {
  isVerified: boolean;
  device: AuthyDeviceModel;
}
export const initialVerifyOtpState: IVerifyOtpState = {
  isVerified: false,
  device: null,
};
