import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { FinancesService } from '../_services/finances.service';
import { Operation } from '../_model/operation';
import { MonthSaldo } from '../_model/monthSaldo';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  saldo = 0;
  operation: Operation;
  monthSaldo: MonthSaldo;

  constructor(private financesService: FinancesService,
              private authService: AuthService) { }

  ngOnInit(): void {
    const userId = this.authService.getCurrentUserId();
    if (!!userId) {
      this.financesService.getSaldo(userId).then(v => this.saldo = v);
      this.financesService.getMonthSaldo(userId, 11, 2020).then(v => this.monthSaldo = v);
    }

  }

  addNewOperation(): void {
    const operation: Operation = new Operation();
    operation.categoryId = '047D875A-C5BE-4B5A-A01B-AFECE14380E1';
    operation.userId = 'C299E36A-773B-40F5-41BB-08D87AB3F647';
    operation.name = 'Paliwo';
    operation.nameOperation = 'WYDATEK';
    operation.price = 825.34;
    operation.created = new Date();

    this.financesService.addOperation(operation);
  }
}
