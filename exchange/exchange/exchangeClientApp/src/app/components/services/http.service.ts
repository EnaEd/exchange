import { HttpClient } from '@angular/common/http';
import { HttpMethodTypeEnum } from '../../enums/http-method-type';

export class HttpService {
  constructor(private httpClient: HttpClient) {}
  request(
    method: HttpMethodTypeEnum,
    url: string,
    headers: any,
    body: any,
    params: any
  ) {
    return this.httpClient.request(method, url, {
      body: body || {},
      headers: headers || {},
      params: params || {},
    });
  }
  get(url: string, headers?: any, data?: any, params?: any) {
    return this.request(HttpMethodTypeEnum.Get, url, data, headers, params);
  }
  post(url: string, headers?: any, data?: any, params?: any) {
    return this.request(HttpMethodTypeEnum.Post, url, data, headers, params);
  }
}
