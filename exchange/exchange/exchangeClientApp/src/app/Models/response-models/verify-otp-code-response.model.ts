import { UserModel } from './../user.model';
import { AuthyDeviceModel } from '../authy-device.model';

export class VerifyOtpCodeResponseModel {
  token: string;
  device: AuthyDeviceModel;
  accessToken: string;
  user: UserModel;
}
