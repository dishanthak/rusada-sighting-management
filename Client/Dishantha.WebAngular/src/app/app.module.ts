import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { DataTablesModule } from 'angular-datatables';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';

import { HttpClientModule } from '@angular/common/http';

import { UIValidationMessageComponent } from './shared/components';
import { UIValidationService } from './shared/services';

import {
  ListSightingComponent,
  ViewSightingComponent,
  CreateSightingComponent,
  EditSightingComponent,
  SightingService,
} from './sightings';

@NgModule({
  declarations: [
    AppComponent,
    UIValidationMessageComponent,
    ListSightingComponent,
    ViewSightingComponent,
    CreateSightingComponent,
    EditSightingComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

    DataTablesModule,
    NgxSpinnerModule,
    ToastrModule.forRoot(),
  ],
  providers: [UIValidationService, SightingService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
