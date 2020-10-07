import { mergeMap, map } from 'rxjs/operators';
import {
  userSelector,
  eventSuccessSelector,
  descriptionEventSelector,
} from './store/auth.selectors';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import * as AuthActions from '../auth/store/auth.actions';
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/app.state';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Actions } from '@ngrx/effects';
import { erorrsSelector } from 'src/app/store/app.selector';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  private error: string[] = [];

  public userSelector$ = this.store.pipe(select(userSelector));

  public erorrsSelector$ = this.store
    .pipe(select(erorrsSelector))
    .subscribe((data) => {
      debugger;
      if (data.length > 0) {
        this.toaster.error(data.toString());
      }
    });

  private _descriptionEvent: string;
  public descriptionEventSelector$ = this.store
    .pipe(select(descriptionEventSelector))
    .subscribe((event) => (this._descriptionEvent = event));
  public eventSuccessSelector$ = this.store
    .pipe(select(eventSuccessSelector))
    .subscribe((data) => {
      if (data) {
        this.toaster.success(this._descriptionEvent);
        this.router.navigateByUrl('/checkotp');
      }
    });

  public phoneForm: FormGroup = new FormGroup({
    phone: new FormControl(''),
  });

  constructor(
    private store: Store<IAppState>,
    private router: Router,
    private toaster: ToastrService
  ) {}

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
