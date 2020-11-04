import { ChatModel } from './../../../Models/chat.model';
export interface IDiscussState {
  rooms: ChatModel[];
  roomChat: ChatModel;
}
export const initialDiscussState: IDiscussState = {
  rooms: null,
  roomChat: null,
};
