import { HttpClient } from '@angular/common/http';
import { Injectable, EventEmitter } from '@angular/core';
import * as SignalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private _hubConnection: SignalR.HubConnection;
  signalRReceived = new EventEmitter();
  constructor() {
    this.buildConnection();
    this.startConnection();
  }

  public buildConnection = () => {
    this._hubConnection = new SignalR.HubConnectionBuilder()
      .withUrl('https://localhost:44310/chat', {
        skipNegotiation: true,
        transport: SignalR.HttpTransportType.WebSockets,
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
