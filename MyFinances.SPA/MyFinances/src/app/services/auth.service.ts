import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { AlertifyService } from './alertify.service';
import { User } from '../model/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient,
              private alertify: AlertifyService,
              private router: Router) { }

  login(model: any): any {
    return this.http.post(environment.authUrl + '/login', model).pipe(map((respone: any) => {
      if (respone) {
        localStorage.setItem('token', respone.token);
        this.decodedToken = this.jwtHelper.decodeToken(respone.token);
      }
    }));
  }

  register(model: User): any {
    return this.http.post(environment.authUrl + '/register', model);
  }

  getCurrentUserId(): string {
    const token = this.jwtHelper.decodeToken(localStorage.getItem('token'));
    return token.nameid;
  }

  getUserName(): string {
    const token = this.jwtHelper.decodeToken(localStorage.getItem('token'));
    return token.user;
  }
  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  redirectToHomeIfLogged(): void {
    if (this.loggedIn()) {
      this.router.navigate(['home']);
    }
  }
}
