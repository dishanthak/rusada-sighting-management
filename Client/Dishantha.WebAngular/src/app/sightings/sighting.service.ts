import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError, finalize } from 'rxjs/operators';

import { environment } from './../../environments/environment';
import { ISighting } from './sighting.model';

@Injectable()
export class SightingService {
  private _baseUrl = environment.baseApiUrl;
  private _apiUrl = `${this._baseUrl}/Sightings`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<ISighting[]> {
    return this.http.get(this._apiUrl).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }

  getById(id: number): Observable<ISighting> {
    return this.http.get(`${this._apiUrl}/${id}`).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }

  search(keyword: string): Observable<ISighting[]> {   
    return this.http.get(this._apiUrl + '/Search?value=' + keyword).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }

  save(data: any): Observable<ISighting> {
    if (data.id === undefined) {
      return this.create(data);
    }
    return this.update(data);
  }

  private create(data: any): Observable<ISighting> {
    return this.http.post(this._apiUrl, data).pipe(
      map((response: any) => {
        return response.data;
      }),
      catchError((error) => {
        return throwError(error);
      })
    );
  }

  private update(data: any): Observable<ISighting> {
    return this.http.put(this._apiUrl, data).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }

  getSearchList(keyword: string): Observable<ISighting[]> {
    return this.http.get(this._apiUrl + '/Search?value=' + keyword).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }
  delete(id: number): Observable<ISighting> {
    return this.http.delete(`${this._apiUrl}/${id}`).pipe(
      map((response: any) => {
        return response.data;
      })
    );
  }
}
