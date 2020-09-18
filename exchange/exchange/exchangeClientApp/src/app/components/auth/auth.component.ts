import { Observable } from 'rxjs';
import {
  userSelector,
  erorrsSelector,
  descriptionEventSelector,
  eventSuccessSelector,
} from './store/auth.selectors';
import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import * as AuthActions from '../auth/store/auth.actions';
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/app.state';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  private error: string[] = [];

  public userSelector$ = this.store.pipe(select(userSelector));

  // public erorrsSelector$ = this.store.pipe(select(erorrsSelector));
  public erorrsSelector$ = this.store.select((state) => state);

  public eventSuccessSelector$ = this.store.pipe(select(eventSuccessSelector));

  public phoneForm: FormGroup = new FormGroup({
    phone: new FormControl(''),
  });

  constructor(private store: Store<IAppState>, private router: Router) {}

  ngOnInit(): void {}
  public onSubmit(): void {
    console.log(this.phoneForm.get('phone').value);
    this.store.dispatch(
      AuthActions.SignInAction({
        //todo add separate method to phone string
        model: {
          countryCode: '+380',
          phoneNumber: '936683442',
        }, //this.phoneForm.get('phone').value,
      })
    );
  }
}
