import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { appRoutes } from './routes';
import { AuthGuard } from './guards/auth.guard';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import {DpDatePickerModule} from 'ng2-date-picker';
import { NavComponent } from './components/nav/nav.component';


import { AuthService } from './services/auth.service';
import { AlertifyService } from './services/alertify.service';
import { FinancesService } from './services/finances.service';
import { JwtModule } from '@auth0/angular-jwt';
import { ChartsModule } from 'ng2-charts';

export function tokkenGetter(): string {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
      AppComponent,
      LoginComponent,
      RegisterComponent,
      HomeComponent,
      NavComponent,
   ],
  imports: [
      BrowserModule,
      RouterModule.forRoot(appRoutes),
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      JwtModule.forRoot({
        config: {
           tokenGetter: tokkenGetter,
           allowedDomains: ['localhost:5000'],
           disallowedRoutes: ['localhost:5000/api/auth']
        }
     }),
     DpDatePickerModule,
     ChartsModule
  ],
  providers: [
    AuthService,
    AuthGuard,
    AlertifyService,
    FinancesService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
