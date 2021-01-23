import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MonthSaldo } from '../model/monthSaldo';
import { environment } from 'src/environments/environment';
import { Statistic } from '../model/statistic';
import { LastTenOperations } from '../model/lastTenOperations';
import { AddOperationDto } from '../model/addOperationDto';
import { AddCategoryDto } from '../model/addCategoryDto';

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

  addOperation(operation: AddOperationDto): any {
    return this.http.post(environment.api + 'Finances/addOperation', operation).toPromise();
  }

  deleteOperation(userId: string, operationId: string): any {
    return this.http.delete(environment.api + 'Finances/deleteOperation?userId=' + userId + '&operationId=' + operationId).toPromise();
  }

  showOperation(userId: string): any {
    return this.http.get(environment.api + 'Finances/getOperations?userId=' + userId).toPromise();
  }

  showLastOperation(userId: string): any {
    return this.http.get<LastTenOperations>(environment.api + 'Finances/GetLastTenOperations?userId=' + userId).toPromise();
  }
  addCategory(category: AddCategoryDto): any {
    return this.http.post(environment.api + 'Finances/addCategory', category).toPromise();
  }
  getCategories(userId: string): any {
    return this.http.get(environment.api + 'Finances/getCategories?userId=' + userId).toPromise();
  }
  showStatistic(userId: string): Promise<Statistic> {
    return this.http.get<Statistic>(environment.api + 'Finances/getStatistic?userId=' + userId).toPromise();
  }
}
