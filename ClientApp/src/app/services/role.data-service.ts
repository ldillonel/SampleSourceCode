import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap, switchMap } from 'rxjs/operators';

// Import models
import { Role } from '../models/role';
import { LogService } from './log.service';

@Injectable({
  providedIn: 'root'
})
export class RoleDataService {

  constructor(
    private logService: LogService,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
    }

  getRoles(): Observable<Role[]> {
    let testing = this.http.get<Role[]>(this.baseUrl + 'api/Roles').pipe(tap(role => this.logService.log('fetched roles')));
    this.logService.log("Received GetRoles: " + testing);
    return testing;
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
