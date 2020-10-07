import { BaseModel } from './../../../Models/base.model';
import { AuthyDeviceModel } from './../../../Models/authy-device.model';
import { IBaseState } from 'src/app/store/app.state';
export interface IVerifyOtpState {
  isVerified: boolean;
  device: AuthyDeviceModel;
}
export const initialVerifyOtpState: IVerifyOtpState = {
  isVerified: false,
  device: null,
};
