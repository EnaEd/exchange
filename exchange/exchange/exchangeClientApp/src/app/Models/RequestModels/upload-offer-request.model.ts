import { UserModel } from './../user.model';
import { OfferOwnerModel } from './../offer-owner.model';
export class UploadOfferRequestModel {
  offerPhoto: string;
  offerDescription: string;
  categoryId: number;
  offerOwnerDetail: OfferOwnerModel;
  userId: number;
  userEntity: UserModel;
}
