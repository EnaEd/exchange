import { UserModel } from './../../../Models/user.model';
import { IBaseState } from 'src/app/store/app.state';
export interface IAuthState {
  user: UserModel;
  isAuthenticate: boolean;
  isUserExists: boolean;
  descriptionEvent: string;
  eventSuccess: boolean;
  authyId: number;
}
export const initialAuthState: IAuthState = {
  user: null,
  isAuthenticate: false,
  isUserExists: false,
  descriptionEvent: null,
  eventSuccess: false,
  authyId: null,
};
