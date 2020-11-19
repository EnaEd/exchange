import { Action, createReducer } from '@ngrx/store';
import {
  IUploadOfferState,
  initialUploadOfferState,
} from './upload-offer.state';

const reducer = createReducer(initialUploadOfferState);
export function uploadReducer(
  state: IUploadOfferState | undefined,
  action: Action
) {
  return reducer(state, action);
}
