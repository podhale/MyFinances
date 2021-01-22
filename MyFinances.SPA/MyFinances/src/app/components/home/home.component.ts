import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FinancesService } from '../../services/finances.service';
import { Operation } from '../../model/operation';
import { Category } from '../../model/calegory';
import { MonthSaldo } from '../../model/monthSaldo';
import { AddOperationDto } from '../../model/addOperationDto';
import { LastTenOperations } from 'src/app/model/lastTenOperations';
import {DatePickerComponent} from 'ng2-date-picker';
import { FormControl, FormGroup } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { Statistic } from 'src/app/model/statistic';
import { AlertifyService } from 'src/app/services/alertify.service';

declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [DatePipe]
})
export class HomeComponent implements OnInit {
  @ViewChild('dayPicker') datePicker: DatePickerComponent;

  months = ['Styczeń', 'Luty', 'Marzec',
            'Kwiecień', 'Maj', 'Czerwiec',
            'Lipiec', 'Sierpień', 'Wrzesień',
            'Październik', 'Listopad', 'Grudzień'];
  saldo = 0;
  isExpanse = true;
  lastTenOperations: LastTenOperations;
  operation: Operation;
  monthSaldo: MonthSaldo;
  statistic: Statistic;
  today = new Date();
  datechart = new Date();
  operations: Operation[];
  categories: Category[];
  selectedDate;
  config;
  public barChartOptions: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: { xAxes: [{}], yAxes: [{}] },
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    }
  };
  public barChartLabels: Label[] = [];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartData: ChartDataSets[] = [
    { data: [0, 0, 0, 0, 0, 0, 0], label: 'Wydatki' },
    { data: [0, 0, 0, 0, 0, 0, 0], label: 'Przychody' }
  ];
  public barChartColors: Color[] = [
    { backgroundColor: '#CF0E0E' },
    { backgroundColor: '#2CB973' },
  ];
  public barChartPlugins = [pluginDataLabels];

  operationForm = new FormGroup({
    name: new FormControl(''),
    categoryId: new FormControl(''),
    date: new FormControl(this.datePipe.transform(this.today, 'yyyy-MM-dd')),
    price: new FormControl(''),
  });


  constructor(private financesService: FinancesService,
              private authService: AuthService,
              private datePipe: DatePipe,
              private alertyfyService: AlertifyService) {
                this.config = {
                  locale: 'pl',
                  format: 'YYYY-MM-DD',
                  maxDate: this.today,
                  mode: '"day"\|"month"\|"time"\|"daytime"'
              }; }

  ngOnInit(): void {
    this.getInfo();
  }

  getInfo(): void {
    const userId = this.authService.getCurrentUserId();
    if (!!userId) {
      this.financesService.getSaldo(userId).then(v => this.saldo = v);
      this.financesService.getCategories(userId).then(v => this.categories = v);
      this.financesService.getMonthSaldo(userId, this.today.getMonth() + 1, this.today.getFullYear()).then(v => this.monthSaldo = v);
      this.financesService.showLastOperation(userId).then(v => {
        this.lastTenOperations = v;
        this.lastTenOperations.expenses = this.lastTenOperations.expenses.sort();
        this.lastTenOperations.income = this.lastTenOperations.income.sort();
      });
      this.getStatistic();
    }
  }

  addOperations(): void {

    if (this.operationForm.valid) {
      let operation = new AddOperationDto();
      operation = this.operationForm.value;
      operation.price = !this.isExpanse ? Number.parseFloat((operation.price * (-1)) + '') : Number.parseFloat(operation.price?.toString());
      operation.userId =  this.authService.getCurrentUserId();

      this.financesService.addOperation(operation).then(() => {
        this.getInfo();
        this.close();
      });
    } else {
      this.alertyfyService.error('Uzupełnij wszytskie pola!');
    }

  }

  open(): void {
    this.datePicker.api.open();
  }
  close(): void  {
    this.datePicker.api.close();
  }

  getStatistic(): void {
    this.financesService.showStatistic(this.authService.getCurrentUserId()).then(x => {
      this.barChartData[0].data = x.expenses;
      this.barChartData[1].data = x.income;
      this.barChartLabels = x.date;
    });
  }

  showModal(isExpanse: boolean): void {

    this.isExpanse = isExpanse;
    $('#modalAddOperations').modal('show');
  }
  cloaseModal(): void {
    $('#modalAddOperations').modal('close');
  }
  // events
  public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
    console.log(event, active);
  }

}
