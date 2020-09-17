import { AuthService } from './components/services/auth.service';
import { HttpService } from './components/services/http.service';
import { BaseGuard } from './components/base/guards/base.guard';
import { appReducers } from './store/app.reducer';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { EffectsModule } from '@ngrx/effects';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { UploadOfferComponent } from './components/upload-offer/upload-offer.component';
import { AuthComponent } from './components/auth/auth.component';
import { HomeComponent } from './components/home/home.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { BaseComponent } from './components/base/base.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthEffects } from '../app/components/auth/store/auth.effects';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    UploadOfferComponent,
    AuthComponent,
    HomeComponent,
    DashboardComponent,
    BaseComponent,
  ],
  imports: [
    ReactiveFormsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    StoreModule.forRoot(appReducers),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
    EffectsModule.forRoot([AuthEffects]),
    StoreRouterConnectingModule.forRoot(),
    FontAwesomeModule,
  ],
  providers: [BaseGuard, AuthService, HttpService],
  bootstrap: [AppComponent],
})
export class AppModule {}
