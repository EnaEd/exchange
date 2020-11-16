import { IAppState } from '../../../../app/store/app.state';
import { createSelector } from '@ngrx/store';

const chatRooms = (state: IAppState) => state.discuss;

export const roomsSelector = createSelector(chatRooms, (state) => state.rooms);
