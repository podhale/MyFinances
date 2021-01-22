import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { OperationsComponent } from './components/operations/operations.component';

export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard], },
  { path: 'operations', component: OperationsComponent, canActivate: [AuthGuard], },

  { path: '**', redirectTo: 'login', pathMatch: 'full' },
];

