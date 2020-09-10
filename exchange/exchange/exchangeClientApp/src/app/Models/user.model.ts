import { PhotoModel } from './photo.model';
import { BaseModel } from './base.model';

export class UserModel extends BaseModel {
  firstName: string;
  lastName: string;
  phone: string;
  country: string;
  city: string;
  email: string;
  password: string;
  oneSignalId: string;
  photos: PhotoModel[];

  constructor(model: UserModel) {
    super(model);
    this.firstName = model.firstName;
    this.lastName = model.lastName;
    this.phone = model.phone;
    this.country = model.country;
    this.city = model.city;
    this.email = model.email;
    this.password = model.password;
    this.oneSignalId = model.oneSignalId;
    this.photos = model.photos;
  }
}
