import {
  Component,
  OnInit,
  ViewChild,
  ElementRef,
  Input,
  Output,
  EventEmitter,
  OnChanges,
} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormControl,
} from '@angular/forms';

import { NgxSpinnerService } from 'ngx-spinner';
import { Subject } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

import { environment } from './../../../environments/environment';
import { UIValidationService } from './../../shared/services';
import { SightingService } from './../sighting.service';
import { ISighting } from './../sighting.model';

@Component({
  selector: 'dishantha-view-sighting',
  templateUrl: './view-sighting.component.html',
  styleUrls: [
    './view-sighting.component.css',
    './view-sighting.component.scss',
  ],
})
export class ViewSightingComponent implements OnInit {
  _pageTitle: string = 'Sightings';
  _pageSubTitle: string = '/ View';
  _form: FormGroup;
  _routeParametersSubscription: any;
  _id: number = 0;
  _sighting: ISighting;
  _aircraftPhoto: any = '../../../assets/images/aircraft-photo.png';

  @Input() selectedSightingId: any;
  @Output() viewSightingModalCloseEvent = new EventEmitter<object>();

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastrService: ToastrService,
    private sightingService: SightingService
  ) {}

  ngOnInit() {
    this._form = this.formBuilder.group({
      make: [{ value: null, disabled: true }],
      model: [{ value: null, disabled: true }],
      registration: [{ value: null, disabled: true }],
      location: [{ value: null, disabled: true }],
      dateAndTime: [{ value: null, disabled: true }],
      aircraftPhoto: [{ value: null, disabled: true }],
    });
  }

  ngOnChanges(changes) {
    if (changes.selectedSightingId) {
      if (this.selectedSightingId != 0) this.get(this.selectedSightingId);
    }
  }

  reset() {
    this.resetForm();
  }

  resetForm() {
    this._form.patchValue({});
  }

  get(id: number): void {
    this.sightingService
      .getById(id)
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(
        (response) => {
          this.onRetrieved(response);
        },
        (error) => {
          this.toastrService.error(error.message);
        }
      );
  }

  onRetrieved(data: any) {
    if (this._form) {
      this._form.reset();
    }

    this._sighting = JSON.parse(JSON.stringify(data || null));

    this._form.patchValue({
      make: this._sighting.make,
      model: this._sighting.model,
      registration: this._sighting.registration,
      location: this._sighting.location,
      dateAndTime: this._sighting.dateAndTime,
    });

    this._aircraftPhoto = this._sighting.aircraftPhoto;
  }

  closeClick(){
    this.viewSightingModalCloseEvent.emit();
  }

}
