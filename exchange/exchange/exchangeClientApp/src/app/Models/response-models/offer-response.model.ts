import { UserModel } from '../user.model';

export class OfferResponseModel {
  photoSource: string;
  description: string;
  categoryId: number;
  userId: number;
  user: UserModel;
}
