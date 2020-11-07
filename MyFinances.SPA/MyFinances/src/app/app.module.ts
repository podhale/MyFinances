import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';

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
  ],
  providers: [
    AuthService,
    AuthGuard,
    AlertifyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
