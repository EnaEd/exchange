import { take } from 'rxjs/operators';
import { async } from '@angular/core/testing';
import { isLogged } from './../../../store/app.selector';
import { IAppState } from './../../../store/app.state';
import { Observable } from 'rxjs';
import {
  Resolve,
  Router,
  ActivatedRouteSnapshot,
  CanActivate,
} from '@angular/router';
import { Store, select } from '@ngrx/store';
import { Injectable } from '@angular/core';

@Injectable()
export class BaseGuard implements CanActivate {
  isLogged: boolean;
  constructor(private router: Router, private store: Store<IAppState>) {}

  isLogged$ = this.store
    .pipe(select(isLogged))
    .subscribe((data: boolean) => (this.isLogged = data));

  canActivate(): boolean {
    debugger;
    if (this.isLogged == false) {
      this.router.navigateByUrl('/auth');
      return false;
    }
    return true;
  }
}
