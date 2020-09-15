export class AuthyUser {
  countryCode: string;
  email: string;
  phone: string;

  constructor(model: AuthyUser) {
    this.countryCode = model.countryCode;
    this.email = model.email;
    this.phone = model.phone;
  }
}
