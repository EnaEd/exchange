import { AuthyUser } from './../../Models/authy-user.model';
import { environment } from './../../../environments/environment';
import { UserModel } from './../../Models/user.model';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
export class AuthService {
  constructor(private httpService: HttpService) {
    //this._client = new Client({ key: 'nhMV4vVdkT8NHP6yu8g6D4JfF8OJw678' });
  }

  signUp(user: UserModel): Observable<any> {
    // return this.httpService.post(
    //   `${environment.apiURL}account/registration`,
    //   null,
    //   user,
    //   null
    // );
    return null;
  }
  checkExistsUser(phone: string): Observable<any> {
    // return this.httpService.post(
    //   `${environment.apiURL}account/checkuserexists`,
    //   null,
    //   phone,
    //   null
    // );
    return null;
  }
  createAuthyUser(model: AuthyUser): Observable<any> {
    // return this._client.registerUser(
    //   {
    //     countryCode: model.countryCode,
    //     email: model.email,
    //     phone: model.phone,
    //   },
    //   (err, res) => {
    //     if (err) throw err;
    //     console.log('Authy Id', res.user.id);
    //   }
    // );
    return null;
  }
  sendrequestSMSCode(model: string): Observable<any> {
    // return this._client.requestSms({ authyId: model }, (err, res) => {
    //   if (err) throw err;
    //   console.log('Message successfully send to', res.cellphone);
    // });
    return null;
  }
}
