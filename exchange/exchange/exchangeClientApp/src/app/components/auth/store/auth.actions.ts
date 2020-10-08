import { UserModel } from './../../../Models/user.model';
import { BaseModel } from './../../../Models/base.model';
import { SignInRequestModel } from './../../../Models/RequestModels/signIn-request.model';
import { AuthyUser } from './../../../Models/authy-user.model';
import { createAction, props } from '@ngrx/store';

export enum AuthActionEnum {
  LoggedSuccess = '[Auth] Confim code successfull, log in ',
  CheckIsUserExist = '[Auth] is user exists',
  CheckIsUserExistSuccess = '[Auth] is user exists success',
  CheckIsUserExistError = '[Auth] is user exists error',
  SignUp = '[Auth] sign up',
  SignUpSuccess = '[Auth] sign up success',
  SignUpError = '[Auth] sign up error',
  SignIn = '[Auth] sign in',
  SignInSuccess = '[Auth] sign in success',
  SignInError = '[Auth] sign in error',
  SignOut = '[Auth] sign out',
  SignOutSuccess = '[Auth] sign out success',
  SignOutError = '[Auth] sign out error',
  SendSMSCode = '[Auth] send sms code',
  SendSMSCodeSuccess = '[Auth] send sms code success',
  RequestsSMSCodeSuccessfull = '[Auth] request sms code successfull',
}
export const LoggedSuccessAction = createAction(AuthActionEnum.LoggedSuccess);

export const RequestsSMSCodeSuccessfullAction = createAction(
  AuthActionEnum.RequestsSMSCodeSuccessfull,
  props<{ payload: string }>()
);
export const SendSMSCodeSuccess = createAction(
  AuthActionEnum.SendSMSCodeSuccess,
  props<{ userId: string }>()
);
export const SendSMSCodeAction = createAction(
  AuthActionEnum.SendSMSCode,
  props<{ authyModel: AuthyUser }>()
);

export const CheckIsUserExistAction = createAction(
  AuthActionEnum.CheckIsUserExist,
  props<{ phoneNumber: string }>()
);
export const CheckIsUserExistSuccessAction = createAction(
  AuthActionEnum.CheckIsUserExistSuccess,
  props<{ payload: boolean }>()
);
export const CheckIsUserExistErrorAction = createAction(
  AuthActionEnum.CheckIsUserExistError,
  props<{ payload: UserModel }>()
);
export const SignInAction = createAction(
  AuthActionEnum.SignIn,
  props<{ model: SignInRequestModel }>()
);
export const SignInSuccessAction = createAction(
  AuthActionEnum.SignInSuccess,
  props<{ payload: string; authyId: string }>()
);
export const SignInErrorAction = createAction(
  AuthActionEnum.SignInError,
  props<{ errors: string[] }>()
);
export const SignUpAction = createAction(
  AuthActionEnum.SignUp,
  props<{ model: UserModel }>()
);
export const SignUpSuccessAction = createAction(
  AuthActionEnum.SignUpSuccess,
  props<{ payload: UserModel }>()
);
export const SignUpErrorAction = createAction(
  AuthActionEnum.SignUpError,
  props<{ payload: UserModel }>()
);
export const SignOutAction = createAction(
  AuthActionEnum.SignOut,
  props<{ payload: UserModel }>()
);
export const SignOutSuccessAction = createAction(AuthActionEnum.SignOutSuccess);
export const SignOutErrorAction = createAction(
  AuthActionEnum.SignOutError,
  props<{ payload: UserModel }>()
);
