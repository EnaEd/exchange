import { CategoryBottomSheet } from './components/home/category-bottom-sheet';
import { HomeEffects } from './components/home/store/home.effects';
import { ExchangeService } from './services/exchange.service';
import { AngularMaterialModule } from './modules/angular-material.module';
import { VerifyOtpEffect } from './components/verify-otp-code/store/verivy-otp.effects';
import { VerifyCodeGuard } from './components/verify-otp-code/guards/verify-code.guard';
import { AuthService } from './services/auth.service';
import { HttpService } from './services/http.service';
import { BaseGuard } from './components/base/guards/base.guard';
import { appReducers, clearReduser } from './store/app.reducer';
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
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthEffects } from '../app/components/auth/store/auth.effects';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { VerifyOTPCodeComponent } from './components/verify-otp-code/verify-otp-code.component';
import { DiscussComponent } from './components/discuss/discuss.component';
import { MatGoogleMapsAutocompleteModule } from '@angular-material-extensions/google-maps-autocomplete';
import { AgmCoreModule } from '@agm/core';
import { OfferDialog } from './components/home/offer-dialog';

@NgModule({
  declarations: [
    AppComponent,
    UploadOfferComponent,
    AuthComponent,
    DashboardComponent,
    BaseComponent,
    HomeComponent,
    VerifyOTPCodeComponent,
    DiscussComponent,
    CategoryBottomSheet,
    OfferDialog,
  ],
  imports: [
    AngularMaterialModule,
    ToastrModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    StoreModule.forRoot(appReducers, { metaReducers: [clearReduser] }),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
    EffectsModule.forRoot([AuthEffects, VerifyOtpEffect, HomeEffects]),
    StoreRouterConnectingModule.forRoot(),
    FontAwesomeModule,
    MatGoogleMapsAutocompleteModule,
    AgmCoreModule.forRoot({
      apiKey: `${environment.googleApiKey}`,
      libraries: [`${environment.googlePlaceLib}`],
      language: `${environment.googleResponseLanguage}`,
    }),
  ],
  providers: [
    BaseGuard,
    AuthService,
    HttpService,
    VerifyCodeGuard,
    ExchangeService,
  ],
  bootstrap: [AppComponent],
  entryComponents: [OfferDialog],
})
export class AppModule {}
