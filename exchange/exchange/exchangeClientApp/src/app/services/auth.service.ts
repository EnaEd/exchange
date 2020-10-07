import { VerifyOtpCodeRequestModel } from './../Models/RequestModels/verify-otp-code-request.model';
import { environment } from './../../environments/environment.prod';
import { SignInRequestModel } from './../Models/RequestModels/signIn-request.model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService {
  constructor(private _client: HttpClient) {}
  signIn(model: SignInRequestModel): Observable<any> {
    return this._client.post(`${environment.apiURL}/account/signin`, model, {});
  }
  verifyOTPCode(model: VerifyOtpCodeRequestModel): Observable<any> {
    debugger;
    console.log('rest');
    return this._client.post(
      `${environment.apiURL}/account/verifycode`,
      model,
      {}
    );
  }
}
