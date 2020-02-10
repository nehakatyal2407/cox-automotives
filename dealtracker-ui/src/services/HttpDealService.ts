import { Injectable } from '@angular/core';
import { Http }       from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable({
  providedIn: 'root',
})
export class HttpDealService {
	private _url = "http://localhost:54446/api";

	constructor(private _http: Http){
	}

	getAllDeals(){
		return this._http.get(`${this._url}/deals`)
			.map(res => res.json());
	}
    
  getTopDeals(){
		return this._http.get(`${this._url}/deals/top-vehicles`)
			.map(res => res.json());
	}
    
  PostDeals(data: FormData){
		return this._http.post(`${this._url}/deals`,data)
			.map(res => res.json());
	}
}