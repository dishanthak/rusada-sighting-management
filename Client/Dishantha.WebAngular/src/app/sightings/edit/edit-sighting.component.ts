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
  selector: 'dishantha-edit-sighting',
  templateUrl: './edit-sighting.component.html',
  styleUrls: [
    './edit-sighting.component.css',
    './edit-sighting.component.scss',
  ],
})
export class EditSightingComponent implements OnInit {
  _pageTitle: string = 'Sightings';
  _pageSubTitle: string = '/ Edit';
  _form: FormGroup;
  _id: number = 0;
  _sighting: ISighting;
  _aircraftPhoto: any = '../../../assets/images/aircraft-photo.png';

  @Input() selectedSightingId: any;
  @Output() editSightingModalCloseEvent = new EventEmitter<object>();
  @Output() sightingEditedEvent = new EventEmitter<object>();

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
      id: [null, [Validators.required]],
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

  ngOnChanges(changes) {
    if (changes.selectedSightingId) {
      if (this.selectedSightingId != 0) this.get(this.selectedSightingId);
    }
  }

  get Sighting() {
    return this._sighting;
  }
  set Sighting(value: any) {
    this._sighting = value;
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
      id: this._sighting.id,
      make: this._sighting.make,
      model: this._sighting.model,
      registration: this._sighting.registration,
      location: this._sighting.location,
      dateAndTime: this._sighting.dateAndTime,
    });

    this._aircraftPhoto = this._sighting.aircraftPhoto;
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
          this.toastrService.info('Record successfully updated.');
          this.sightingEditedEvent.emit();
        },
        (error) => {
          this.toastrService.error(error.message);
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
    this.editSightingModalCloseEvent.emit();
  }

}
