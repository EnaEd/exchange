import { VerifyCodeGuard } from './components/verify-otp-code/guards/verify-code.guard';
import { BaseGuard } from './components/base/guards/base.guard';
import { BaseComponent } from './components/base/base.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { HomeComponent } from './components/home/home.component';
import { VerifyOTPCodeComponent } from './components/verify-otp-code/verify-otp-code.component';

const routes: Routes = [
  {
    path: '',
    component: BaseComponent,
    children: [{ path: '', component: HomeComponent }],
    canActivate: [BaseGuard],
  },
  {
    path: 'home',
    component: BaseComponent,
    canActivate: [BaseGuard],
    canActivateChild: [BaseGuard],
    children: [{ path: '', component: HomeComponent }],
  },
  { path: 'auth', component: AuthComponent },
  {
    path: 'checkotp',
    component: VerifyOTPCodeComponent,
    canActivate: [VerifyCodeGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
