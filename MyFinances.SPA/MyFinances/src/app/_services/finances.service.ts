import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MonthSaldo } from '../_model/monthSaldo';
import { environment } from 'src/environments/environment';
import { Operation } from '../_model/operation';

@Injectable({
  providedIn: 'root'
})
export class FinancesService {

  constructor(private http: HttpClient) { }

  getSaldo(userId: string): Promise<number> {
    return this.http.get<number>(environment.api + 'Finances/saldo?userId=' + userId).toPromise();
  }

  getMonthSaldo(userId: string, month: number, year: number): Promise<MonthSaldo> {
    return this.http.get<MonthSaldo>(environment.api + 'Finances/monthSaldo?userId=' + userId
                                                                          + '&month=' + month
                                                                          + '&year=' + year).toPromise();
  }

  addOperation(operation: Operation): any {
    return this.http.post(environment.api + 'Finances/addOperation', operation).toPromise();
  }
}
