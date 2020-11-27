import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';


import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { FinancesService } from './_services/finances.service';
import { JwtModule } from '@auth0/angular-jwt';

export function tokkenGetter(): string {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [	
      AppComponent,
      LoginComponent,
      RegisterComponent,
      HomeComponent,
      NavComponent
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
