import { ChatModel } from './../../../Models/chat.model';
import { createAction, props } from '@ngrx/store';
export enum IDiscussActionEnum {
  GetChatRooms = '[Discuss action] Get chat rooms',
  GetChatRoomsSuccess = '[Discuss action] Get chat rooms success',
}

export const GetChatRoomsAction = createAction(
  IDiscussActionEnum.GetChatRooms,
  props<{ client: number }>()
);
export const GetChatRoomsSuccessAction = createAction(
  IDiscussActionEnum.GetChatRoomsSuccess,
  props<{ rooms: ChatModel[] }>()
);
