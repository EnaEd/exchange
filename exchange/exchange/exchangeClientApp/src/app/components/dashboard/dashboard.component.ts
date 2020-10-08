import { UserModel } from 'src/app/Models/user.model';
import * as AuthActions from '../auth/store/auth.actions';
import { IAppState } from 'src/app/store/app.state';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { userSelector } from '../auth/store/auth.selectors';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  user: UserModel;
  user$ = this._store.pipe(
    select(userSelector),
    map((data) => {
      this.user = data ?? null;
      return data;
    })
  );

  constructor(private _router: Router, private _store: Store<IAppState>) {}

  ngOnInit(): void {}

  navigateToExchange() {
    this._router.navigateByUrl('home');
  }
  navigateToDiscuss() {
    this._router.navigateByUrl('discuss');
  }
  navigateToUpload() {
    this._router.navigateByUrl('upload');
  }
  SignOut() {
    this._store.dispatch(AuthActions.SignOutAction({ payload: this.user }));
  }
}
