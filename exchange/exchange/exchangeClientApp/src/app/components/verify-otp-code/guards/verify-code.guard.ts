import { empty } from 'rxjs';
import { descriptionEventSelector } from './../../auth/store/auth.selectors';
import { IAppState } from '../../../store/app.state';
import { Store, select } from '@ngrx/store';
import { CanActivate, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';

@Injectable()
export class VerifyCodeGuard implements CanActivate {
  private _descriptionEvent: string;
  descriptionEvent$ = this._store
    .pipe(select(descriptionEventSelector))
    .subscribe((data) => {
      this._descriptionEvent = data;
    });

  constructor(
    private _router: Router,
    private _store: Store<IAppState>,
    private _toaster: ToastrService
  ) {}

  canActivate(): boolean {
    if (
      this._descriptionEvent ??
      ''.normalize().includes('sms token was sent'.normalize())
    ) {
      return true;
    }
    //TODO EE:uncomment after logic complite

    // this._toaster.warning('code was not send');
    // this._router.navigateByUrl('/auth');
    // return false;
    console.log('in guard check otp');
    return true;
  }
}
