import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import * as AuthActions from '../auth/store/auth.actions';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  public phoneForm: FormGroup = new FormGroup({
    phone: new FormControl(''),
  });

  constructor(private store: Store<IAppState>) {}

  ngOnInit(): void {}
  public onSubmit(): void {
    console.log(this.phoneForm.get('phone').value);
    this.store.dispatch(
      AuthActions.SignInAction({
        //todo add separate method to phone string
        model: {
          countryCode: '+380',
          phoneNumber: '936683441',
        }, //this.phoneForm.get('phone').value,
      })
    );
  }
}
