import { UserModel } from './../../../Models/user.model';
export interface IAuthState {
  user: UserModel;
  isAuthenticate: boolean;
  isUserExists: boolean;
  descriptionEvent: string;
}
export const initialAuthState: IAuthState = {
  user: null,
  isAuthenticate: false,
  isUserExists: false,
  descriptionEvent: null,
};
