import { UserModel } from './../../../Models/user.model';
export interface IAuthState {
  user: UserModel;
  isAuthenticate: boolean;
  isUserExists: boolean;
}
export const initialAuthState: IAuthState = {
  user: null,
  isAuthenticate: false,
  isUserExists: false,
};
