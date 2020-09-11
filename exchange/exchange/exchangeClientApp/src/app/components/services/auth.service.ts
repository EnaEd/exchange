import { UserModel } from './../../Models/user.model';
import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
export class AuthService {
  constructor(private httpService: HttpService) {}

  signUp(user: UserModel): Observable<any> {
    return this.httpService.post(
      `${environment.apiURL}account/registration`,
      null,
      user,
      null
    );
  }
  checkExistsUser(phone: string): Observable<any> {
    return this.httpService.post(
      `${environment.apiURL}account/checkuserexists`,
      null,
      phone,
      null
    );
  }
}
