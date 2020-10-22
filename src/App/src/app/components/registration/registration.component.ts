import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import { countries } from '../../../constants/countries'
import { catalogs } from '../../../constants/catalogs'
import { samples } from '../../../constants/samples'
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { Router } from "@angular/router";
import { CustomValidationService } from '../services/customValidation.service';
import { HttpService } from '../services/http.service';

@Component({
	selector: 'app-registration',
	templateUrl: './registration.component.html',
	styleUrls: ['./registration.component.scss'],
	providers: [HttpService]
})
export class RegistrationComponent implements OnInit {
	registration: FormGroup;
	countries: { text: string, value: string}[] = countries;
	filteredCountries: Observable<{ "text": string, "value": string }[]>;
	catalogs: string[] = catalogs;
	samples: string[] = samples;
	disabledSubmitTooltip = 'Fill the form to submit';
	disableSubmit = false;

	constructor(private router: Router,
	            private formBuilder: FormBuilder,
	            private customValidator: CustomValidationService,
	            private httpService: HttpService) {
	}

	ngOnInit(): void {
		this.registration = this.formBuilder.group({
			firstName: ['', [
				Validators.pattern(/^[a-zA-Z\s,-]{2,}/),
				Validators.required
			]],
			lastName: ['', [
				Validators.pattern(/^[a-zA-Z\s,-]{2,}/),
				Validators.required
			]],
			country: ['', [
				Validators.required,
				this.customValidator.countryValidator.bind(this.customValidator)]],
			email: ['', [
				Validators.email,
				Validators.required
			]],
			password: ['', Validators.compose([
				Validators.pattern(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$/),
				Validators.required])],
			confirmPassword: ['', [Validators.required]],
			tel: ['', [
				Validators.pattern(/^([0-9]{3}-){2}[0-9]{4}$/),
				Validators.required
			]],
			date: ['', [
				Validators.required,
				this.customValidator.dateOfBirthValidator.bind(this.customValidator)]],
			gender: ['male'],
			address: ['', Validators.required],
			catalogs: new FormArray([]),
			samples: ['Herbs'],
			avatar: new FormControl("",    []),
			agreement: [true, Validators.requiredTrue],
		}, {
			validator: this.customValidator.confirmPasswordValidator()
		});

		this.filteredCountries = this.registration.controls.country.valueChanges
		.pipe(
			startWith(''),
			map(value => this.filterCountries(value))
		);
	}

	get controls(): { [key: string]: AbstractControl } {
		return this.registration.controls;
	}

	private filterCountries(value: string): { "text": string, "value": string }[] {
		const filterValue = value && value.toLowerCase();
		return this.countries.filter(country => country.text.toLowerCase().includes(filterValue));
	}

	public changeCatalogTypes(event, type: string): void {
		const formArray: FormArray = this.registration.get('catalog_types') as FormArray;

		if (event.checked) {
			formArray.push(new FormControl(type));
		} else {
			let i: number = 0;
			formArray.controls.forEach((control: FormControl) => {
				if(control.value == type) {
					formArray.removeAt(i);
					return;
				}
				i++;
			});
		}
	}

	public submit(): void {
		console.log(this.registration.controls);
		if (this.registration.invalid) {
			this.disableSubmit = true;
			return
		}

		this.disableSubmit = false;
		const registration = this.controls;

		this.httpService.postData(registration).subscribe(
			(event) => {
				console.log(event);
				this.reset();
				},
			error => console.log(error)
		);
	}

	private reset(): void {
		this.registration.reset();
		for (let name in this.controls) {
			this.controls[name].setErrors(null);
		}
		this.controls.gender.setValue('male');
		this.controls.samples.setValue('Herbs');
		this.controls.agreement.setValue(true);
	}
}
