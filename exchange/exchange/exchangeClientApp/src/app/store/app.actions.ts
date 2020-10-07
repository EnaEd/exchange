import { createAction, props } from '@ngrx/store';
export enum BaseActionEnum {
  ErrorActionEnum = '[Base Action] Base error action',
  ClearErrorActionEnum = '[Base Action] Clear Error',
}
export const BaseErrorAction = createAction(
  BaseActionEnum.ErrorActionEnum,
  props<{ errors: string[] }>()
);
export const BaseClearErrorAction = createAction(
  BaseActionEnum.ClearErrorActionEnum
);
