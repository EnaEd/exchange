import { AuthyDeviceModel } from '../authy-device.model';

export class VerifyOtpCodeResponseModel {
  token: number;
  device: AuthyDeviceModel;
}
