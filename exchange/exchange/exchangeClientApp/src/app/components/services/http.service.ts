import { HttpClient } from '@angular/common/http';
import { HttpMethodTypeEnum } from '../../enums/http-method-type';
import { Injectable } from '@angular/core';

@Injectable()
export class HttpService {
  private _headers: any = {
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Headers':
      'Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With',
  };
  constructor(private httpClient: HttpClient) {}
  request(
    method: HttpMethodTypeEnum,
    url: string,
    headers: any,
    body: any,
    params: any
  ) {
    return this.httpClient.request(method, url, {
      headers,
      body: body || {},
      params: params || {},
    });
  }
  get(url: string, data?: any, params?: any) {
    return this.request(
      HttpMethodTypeEnum.Get,
      url,
      this._headers,
      data,
      params
    );
  }
  post(url: string, data?: any, params?: any) {
    debugger;
    return this.request(
      HttpMethodTypeEnum.Post,
      url,
      this._headers,
      data,
      params
    );
  }
}
