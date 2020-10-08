import { UploadOfferComponent } from './components/upload-offer/upload-offer.component';
import { DiscussComponent } from './components/discuss/discuss.component';
import { VerifyCodeGuard } from './components/verify-otp-code/guards/verify-code.guard';
import { BaseGuard } from './components/base/guards/base.guard';
import { BaseComponent } from './components/base/base.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './components/auth/auth.component';
import { HomeComponent } from './components/home/home.component';
import { VerifyOTPCodeComponent } from './components/verify-otp-code/verify-otp-code.component';

const routes: Routes = [
  {
    path: '',
    component: BaseComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'discuss', component: DiscussComponent },
      { path: 'home', component: HomeComponent },
      { path: 'upload', component: UploadOfferComponent },
    ],
    //canActivateChild: [BaseGuard],
    canActivate: [BaseGuard],
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
