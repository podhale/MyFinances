import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../_model/user';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  user: User;

  constructor(public authService: AuthService,
              private fb: FormBuilder,
              private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit(): void {
    this.authService.redirectToHomeIfLogged();
    this.createLoginForm();
  }

  createLoginForm(): void {
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', [Validators.required]],
    });
  }

  login(): void {
    if (this.loginForm.valid) {
      this.user = Object.assign({}, this.loginForm.value);

      this.authService.login(this.user).subscribe(() => {
        this.alertify.success('Zostałeś zalogowany!');
        this.router.navigate(['home']);
      }, error => {
        this.alertify.error(error.error);
      });
    }
  }
}
