import { IAppState } from './../store/app.state';
import { Store } from '@ngrx/store';
import { HttpClient } from '@angular/common/http';
import { Injectable, EventEmitter, Inject } from '@angular/core';
import * as SignalR from '@aspnet/signalr';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private _hubConnection: SignalR.HubConnection;
  signalRReceived = new EventEmitter();
  constructor(@Inject(LOCAL_STORAGE) private _storage: StorageService) {
    this.buildConnection();
    this.startConnection();
  }

  public buildConnection = () => {
    let test = this._storage.get('accessToken');
    debugger;
    this._hubConnection = new SignalR.HubConnectionBuilder()
      .withUrl('https://localhost:44310/chat', {
        skipNegotiation: true,
        transport: SignalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => this._storage.get('accessToken'),
      })

      .build();
  };
  public startConnection = () => {
    debugger;
    this._hubConnection
      .start()
      .then(() => {
        console.log('connection started...');
        this.registerSignalREvents();
      })
      .catch((error) => {
        console.log(`connection error ${error}`);

        //reconnect after 3 sec
        setTimeout(function () {
          this.startConnection();
        }, 3000);
      });
  };

  registerSignalREvents() {
    //TODO EE: need dispatch chat room and messages
    this._hubConnection.on('Receive', () => {
      console.log('need dispatch chat room and messages');
    });
  }
}
