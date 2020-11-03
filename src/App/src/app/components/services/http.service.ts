import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { setFormData } from '../../../utils/setFormData'

@Injectable({
	providedIn: 'root'
})
export class HttpService {
	constructor(private http: HttpClient){ }

	postData(orderData) {
		const body = setFormData(orderData);
		return this.http.post('http://localhost:4200/order', body, {
			reportProgress: true,
			observe: 'events'
		});
	}
}
