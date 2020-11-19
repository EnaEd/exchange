import { UploadOfferRequestModel } from '../../../Models/RequestModels/upload-offer-request.model';

export interface IUploadOfferState {
  offer: UploadOfferRequestModel;
}

export const initialUploadOfferState: IUploadOfferState = {
  offer: null,
};
