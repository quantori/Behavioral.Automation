import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';

import { countries } from '../../../constants/countries'

@Injectable({
	providedIn: 'root'
})
export class CustomValidationService {

	countryValidator(control: AbstractControl): { invalidCountryName: boolean } {
		const validCountry =  countries.some(country => country.text ===  control.value);
		return validCountry ? null : { invalidCountryName: true };
	}

	confirmPasswordValidator(): (formGroup: FormGroup) => void {
		return (formGroup: FormGroup) => {
			const control = formGroup.controls['password'];
			const matchingControl = formGroup.controls['confirmPassword']
			if (matchingControl.errors && !matchingControl.errors.passwordMismatch) {
				return;
			}
			if (control.value !== matchingControl.value) {
				matchingControl.setErrors({ passwordMismatch: true });
			} else {
				matchingControl.setErrors(null);
			}
		};
	}

	dateOfBirthValidator(control: AbstractControl): { invalidDateOfBirth: boolean } {
		if (!control.value) {
			return
		}
		const ageOfMajority =  18;
		const date = new Date();
		date.setMonth(date.getMonth() - (12 * ageOfMajority));
		const value = Date.parse(control.value);
		return value <= date.getTime() ? null : { invalidDateOfBirth: true };
	}
}
