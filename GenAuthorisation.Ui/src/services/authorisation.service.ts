import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserToken } from '../models/user-token';
import { Settings } from '../settings';

@Injectable({
  providedIn: 'root'
})
export class AuthorisationService {
  private urlPrefix: string = '';

  constructor(private http: HttpClient, private settings: Settings) {
    this.urlPrefix = this.settings.domain + '/api';
  }

  /** POST **/
  validateUser(userToken: UserToken) {
    var httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post<string>(this.urlPrefix + '/Validation/Validate', JSON.stringify(userToken, (_, v) => typeof v === 'bigint' ? v.toString() : v), httpOptions).pipe(
      catchError(this.handleError<string>('validateUser'))
    ).toPromise();
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      this.log(`${operation} failed: ${error.message}`);

      confirm("Please verify either email or phone to login");

      return throwError(error);
    };
  }

  private log(message: string) {
    console.log(`AuthenticationService: ${message}`);
  }
}
