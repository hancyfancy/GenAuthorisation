import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app/app.component';
import { ValidateComponent } from './validate/validate.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { TokenLengthValidatorDirective } from '../validators/token-length-validator.directive';

@NgModule({
  declarations: [
    AppComponent,
    ValidateComponent,
    NotFoundComponent,
    TokenLengthValidatorDirective
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
