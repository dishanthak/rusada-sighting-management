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

import { UIValidationService } from './../../shared/services';
import { SightingService } from './../sighting.service';
import { ISighting } from './../sighting.model';

@Component({
  selector: 'dishantha-create-sighting',
  templateUrl: './create-sighting.component.html',
  styleUrls: [
    './create-sighting.component.css',
    './create-sighting.component.scss',
  ],
})
export class CreateSightingComponent implements OnInit {
  _pageTitle: string = 'Sightings';
  _pageSubTitle: string = '/ New';
  _form: FormGroup;
  _id: number = 0;
  _sighting: ISighting;
  _aircraftPhoto: any = '../../../assets/images/aircraft-photo.png';

  @Output() sightingCreatedEvent = new EventEmitter<object>();
  @Output() createSightingModalCloseEvent = new EventEmitter<object>();

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
      make: [null, [Validators.required, Validators.maxLength(128)]],
      model: [null, [Validators.required, Validators.maxLength(128)]],
      registration: [
        null,
        [
          Validators.required,
          Validators.maxLength(128),
          Validators.pattern('^[a-zA-Z]{1,2}-[a-zA-Z]{1,5}$'),
        ],
      ],
      location: [null, [Validators.required, Validators.maxLength(255)]],
      dateAndTime: [
        null,
        [Validators.required, UIValidationService.pastDateTimeValidator()],
      ],
      aircraftPhoto: [null],
    });
  }

  get Sighting() {
    return this._sighting;
  }
  set Sighting(value: any) {
    this._sighting = value;
  }

  resetClick() {
    this.resetForm();
  }

  resetForm() {
    this._form.reset({});
    this._aircraftPhoto = '../../../assets/images/aircraft-photo.png';
  }

  saveClick() {
    if (!this._form.valid) {
      this.validateAllFormFields(this._form);
      this.toastrService.info('Form is not valid.');
      return;
    }

    this.onSave();
  }

  onSave() {
    this.spinner.show();
    let saveObject = Object.assign({}, this.Sighting, this._form.value);

    saveObject.aircraftPhoto = this._aircraftPhoto;

    this.sightingService
      .save(saveObject)
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(
        (response) => {
          this.toastrService.info('Record successfully created.');
          this.sightingCreatedEvent.emit();
        },
        (error) => {
          //this.showMessage(SeverityLevel.Error, error);
        }
      );
  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach((field) => {
      let control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  loadImage(event: File[]) {

    const files = event;
    if (files.length === 0) return;

    const mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.toastrService.info('Only images are supported.');
      return;
    }

    let reader = new FileReader();
    if (event && event.length > 0) {
      let file = event[0];
      reader.readAsDataURL(file);
      reader.onload = () => {
        this._aircraftPhoto = reader.result;
      };
    }
  }

  clearPhotoClick() {
    this._aircraftPhoto = '../../../assets/images/aircraft-photo.png';
  }

  closeClick(){
    this.createSightingModalCloseEvent.emit();
  }

}
