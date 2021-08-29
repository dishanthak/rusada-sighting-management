import {
  ValidatorFn,
  AbstractControl,
  FormGroup,
  FormControl,
} from '@angular/forms';
import * as moment from 'moment';

export class UIValidationService {
  static getValidatorErrorMessage(validatorName: string, validatorValue?: any) {
    let config: { [unit: string]: string } = {
      required: 'Required',
      invalidCreditCard: 'Invalid credit card number',
      invalidEmailAddress: 'Invalid email address',
      invalidPassword:
        'Invalid password. Password must be at least 6 characters long, and contain a number.',
      minlength: `Minimum length must be at least ${validatorValue.requiredLength} characters`,
      maxlength: `Maximum length cannot exceed ${validatorValue.requiredLength} characters`,
      range: 'Value between 1 (lowest) and 5 (highest).',
      pattern: 'Missed match the pattern',
      invalidPastDateTime: 'Invalid past Date and Time',
    };

    return config[validatorName];
  }

  static rangeValidator(control: any, min: number, max: number) {
    if (
      control.value &&
      (isNaN(control.value) || control.value < min || control.value > max)
    ) {
      return null;
    } else {
      return { range: true };
    }
  }

  static creditCardValidator(control: any) {
    // Visa, MasterCard, American Express, Diners Club, Discover, JCB
    if (
      control.value.match(
        /^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/
      )
    ) {
      return null;
    } else {
      return { invalidCreditCard: true };
    }
  }

  static emailValidator(control: any) {
    if (!control.value) {
      return { invalidEmailAddress: true };
    }
    // RFC 2822 compliant regex
    if (
      control.value.match(
        /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/
      )
    ) {
      return null;
    } else {
      return { invalidEmailAddress: true };
    }
  }

  static passwordValidator(control: any) {
    // {6,100}           - Assert password is between 6 and 100 characters
    // (?=.*[0-9])       - Assert a string has at least one number
    if (control.value.match(/^(?=.*[0-9])[a-zA-Z0-9!@#$%^&*]{6,100}$/)) {
      return null;
    } else {
      return { invalidPassword: true };
    }
  }

  static pastDateTimeValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } => {
      if (!control.value) {
        return { invalidPastDateTime: true };
      }

      let valueTime = moment(control.value).format('DD/MM/YYYY HH:mm');

      let valueNow = moment().format('DD/MM/YYYY HH:mm');

      if (valueNow <= valueTime) {
        return { invalidPastDateTime: true };
      }

      return null;
    };
  }
}
