import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';

import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import {
  SightingService,
  ISighting,
  ViewSightingComponent,
  CreateSightingComponent,
  EditSightingComponent,
} from './../../sightings';

@Component({
  templateUrl: 'list-sighting.component.html',
  styleUrls: ['list-sighting.component.css', 'list-sighting.component.scss'],
})
export class ListSightingComponent implements OnInit {
  _pageTitle: string = 'Sightings';
  _pageSubTitle: string = '/ List';
  _sightings: ISighting[];
  _id: number = 0;
  _id_view_data: number = 0;
  _id_edit_data: number = 0;
  _keyword: string = '';

  @ViewChild('id_viewSighting', { static: true })
  _viewSightingComponent: ViewSightingComponent;

  @ViewChild('id_editSighting', { static: true })
  _editSightingComponent: EditSightingComponent;

  @ViewChild('id_createSighting', { static: true })
  _createSightingComponent: CreateSightingComponent;

  @ViewChild('id_viewSightingCloseButton', { static: true })
  _viewSightingCloseButton: ElementRef;

  @ViewChild('id_createSightingCloseButton', { static: true })
  _createSightingCloseButton: ElementRef;

  @ViewChild('id_editSightingCloseButton', { static: true })
  _editSightingCloseButton: ElementRef;

  @ViewChild('id_deleteSightingCloseButton', { static: true })
  _deleteSightingCloseButton: ElementRef;

  constructor(
    private sightingService: SightingService,
    private spinner: NgxSpinnerService,
    private toastrService: ToastrService
  ) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.spinner.show();
    this._sightings = [];
    this.sightingService
      .getAll()
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(
        (response: any) => {
          this._sightings = response;
        },
        (error: any) => {
          this.toastrService.error(error.message);
        }
      );
  }

  searchClick() {
    if (this._keyword == '') return;

    this.spinner.show();
    this._sightings = [];
    this.sightingService
      .search(this._keyword)
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(
        (response: any) => {
          this._sightings = response;
        },
        (error: any) => {
          this.toastrService.error(error.message);
        }
      );
  }

  clearSearchClick() {
    this._keyword = '';
    this.loadData();
  }

  createClick() {
    this._createSightingComponent.ngOnInit();
  }

  viewClick(data: any) {
    this._id_view_data = data.id;
    this._viewSightingComponent.ngOnInit();
  }

  editClick(data: any) {
    this._id_edit_data = data.id;
    this._editSightingComponent.ngOnInit();
  }

  deleteClick(data: any) {
    this._id = data.id;
  }

  deleteConfirmed() {
    this.spinner.show();

    this.sightingService
      .delete(this._id)
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(
        (response) => {
          this.toastrService.info('Record successfully deleted.');
          this.loadData();
          this.deleteSightingModalCloseEventHandler();
        },
        (error) => {
          this.toastrService.error(error.message);
        }
      );
  }

  refreshClick() {
    this._keyword = '';
    this.loadData();
  }

  viewSightingModalCloseEventHandler() {
    this._viewSightingCloseButton.nativeElement.click();
    this._id_view_data = 0;
  }

  sightingCreatedEventEventHandler() {
    this._createSightingCloseButton.nativeElement.click();
    this.loadData();
  }

  createSightingModalCloseEventHandler() {
    this._createSightingCloseButton.nativeElement.click();
    this._id = 0;
  }

  editSightingModalCloseEventHandler() {
    this._editSightingCloseButton.nativeElement.click();
    this._id_edit_data = 0;
  }

  sightingEditedEventEventHandler() {
    this._editSightingCloseButton.nativeElement.click();
    this._id = 0;
    this.loadData();
  }

  deleteSightingModalCloseEventHandler() {
    this._deleteSightingCloseButton.nativeElement.click();
    this._id = 0;
  }
}
