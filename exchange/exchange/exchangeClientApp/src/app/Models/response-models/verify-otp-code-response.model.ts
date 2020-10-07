import { AuthyDeviceModel } from '../authy-device.model';

export class VerifyOtpCodeResponseModel {
  token: string;
  device: AuthyDeviceModel;
}
