import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CurrencyModel } from './CurrencyModel';

@Injectable({
  providedIn: 'root'
})
export class AppEndpointsService {
  private baseURL = 'https://api.nbp.pl/api/exchangerates/rates/A/'
  constructor(private http: HttpClient) {
    
  }
  public getCurrency(currency: string) {
    return this.http.get<CurrencyModel>(this.baseURL + currency + '/2023-10-12')
  }
}


