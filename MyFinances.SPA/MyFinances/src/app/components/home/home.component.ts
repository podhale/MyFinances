import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FinancesService } from '../../services/finances.service';
import { Operation } from '../../model/operation';
import { MonthSaldo } from '../../model/monthSaldo';
import { LastTenOperations } from 'src/app/model/lastTenOperations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  months = ['Styczeń', 'Luty', 'Marzec',
            'Kwiecień', 'Maj', 'Czerwiec',
            'Lipiec', 'Sierpień', 'Wrzesień',
            'Październik', 'Listopad', 'Grudzień'];
  saldo = 0;
  lastTenOperations: LastTenOperations;
  operation: Operation;
  monthSaldo: MonthSaldo;
  today = new Date();
  operations: Operation[];

  constructor(private financesService: FinancesService,
              private authService: AuthService) { }

  ngOnInit(): void {
    const userId = this.authService.getCurrentUserId();
    if (!!userId) {
      this.financesService.getSaldo(userId).then(v => this.saldo = v);
      this.financesService.getMonthSaldo(userId, this.today.getMonth() + 1, this.today.getFullYear()).then(v => this.monthSaldo = v);
      this.financesService.showLastOperation(userId).then(v => {
        this.lastTenOperations = v;
        this.lastTenOperations.expenses = this.lastTenOperations.expenses.sort();
        this.lastTenOperations.income = this.lastTenOperations.income.sort();
      });
    }

  }
}
