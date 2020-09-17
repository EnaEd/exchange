import { environment } from './../../../environments/environment.prod';
import { SignInRequestModel } from './../../Models/RequestModels/signIn-request.model';
import { AuthyUser } from './../../Models/authy-user.model';

import { UserModel } from './../../Models/user.model';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class AuthService {
  // constructor(private _httpService: HttpService) {}

  // signIn(model: SignInRequestModel): Observable<any> {
  //   debugger;
  //   return this._httpService.post(
  //     `${environment.apiURL}/account/signin`,
  //     model
  //   );
  // }

  constructor(private _client: HttpClient) {}
  signIn(model: SignInRequestModel): Observable<any> {
    debugger;
    const headers = new HttpHeaders()
      .set(
        'Access-Control-Allow-Headers',
        'Content-Type,Access-Control-Allow-Headers, Authorization, X-Requested-With'
      )
      .set('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE')
      .set('Access-Control-Allow-Origin', '*');
    return this._client.post(`${environment.apiURL}/account/signin`, model, {
      headers: headers,
    });
  }
}
