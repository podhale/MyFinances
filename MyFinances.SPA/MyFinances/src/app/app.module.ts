import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
      AppComponent,
      LoginComponent,
      RegisterComponent
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
    AlertifyService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
