import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { User } from '../_model/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  user: User;

  constructor(public authService: AuthService,
              private fb: FormBuilder,
              private alertify: AlertifyService) { }

  ngOnInit(): void {
    this.authService.redirectToHomeIfLogged();
    this.createRegisterForm();
  }

  register(): void {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      console.log(this.user);

      this.authService.register(this.user).subscribe(() => {
        this.alertify.success('Rejestracja udana');
      }, error => {
        this.alertify.error(error.error);
      });
    }
  }

  createRegisterForm(): void {
    this.registerForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(12)]],
      confirmPassword: ['', Validators.required],
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(fg: FormControl): any {
    return fg.get('password').value === fg.get('confirmPassword').value ? null : { mismatch: true };
  }

}
