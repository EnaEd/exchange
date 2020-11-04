import { ChatRequestModel } from 'src/app/Models/RequestModels/chat-request.model';
import { ChatModel } from './../Models/chat.model';
import { environment } from './../../environments/environment';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class DiscussService {
  constructor(private _httpService: HttpService) {}

  getRooms(clientId: number): Observable<ChatModel[]> {
    return this._httpService.post(
      `${environment.apiURL}/messanger/getchats`,
      clientId
    ) as Observable<ChatModel[]>;
  }
  createChatRoom(model: ChatRequestModel): Observable<ChatModel> {
    return this._httpService.post(
      `${environment.apiURL}/messanger/createchat`,
      model
    ) as Observable<ChatModel>;
  }
}
