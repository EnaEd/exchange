import { roomsSelector } from './store/discuss.selectors';
import { IAppState } from 'src/app/store/app.state';
import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import * as DiscussActions from '../discuss/store/discuss.actions';

@Component({
  selector: 'app-discuss',
  templateUrl: './discuss.component.html',
  styleUrls: ['./discuss.component.css'],
})
export class DiscussComponent implements OnInit {
  constructor(private _store: Store<IAppState>) {}

  chatRooms$ = this._store.pipe(select(roomsSelector));
  ngOnInit(): void {
    this._store.dispatch(DiscussActions.GetChatRoomsAction({ client: 22 }));
  }

  ToDiscuss() {}
}
