export class SignInRequestModel {
  phoneNumber: string;
  countryCode: string;

  constructor(model: SignInRequestModel) {
    this.countryCode = model.countryCode;
    this.phoneNumber = model.phoneNumber;
  }
}
