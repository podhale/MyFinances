import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { AlertifyService } from './alertify.service';
import { User } from '../_model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient,
              private alertify: AlertifyService) { }

  login(model: any): any {
    return this.http.post(environment.authUrl + '/login', model).pipe(map((respone: any) => {
      const user = respone;
      if (user) {
        localStorage.setItem('token', user.token);
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
      }
    }));
  }

  register(model: User): any {
    return this.http.post(environment.authUrl + '/register', model);
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
