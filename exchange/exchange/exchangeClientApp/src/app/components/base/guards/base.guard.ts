import { isLoggedSelector } from './../../../store/app.selector';
import { IAppState } from './../../../store/app.state';
import { Router, CanActivate } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Injectable } from '@angular/core';

@Injectable()
export class BaseGuard implements CanActivate {
  isLogged: boolean;
  constructor(private router: Router, private store: Store<IAppState>) {}

  isLogged$ = this.store
    .pipe(select(isLoggedSelector))
    .subscribe((data: boolean) => (this.isLogged = data));

  canActivate(): boolean {
    // TODO: Uncomment after complete app
    if (this.isLogged == false) {
      this.router.navigateByUrl('/auth');
      return false;
    }
    return true;
  }
}
