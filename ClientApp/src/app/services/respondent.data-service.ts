import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of, forkJoin, merge, concat } from "rxjs";
import { catchError, tap, map } from "rxjs/operators";
import { RoleDataService } from './role.data-service';
import { ToastrService } from "ngx-toastr";

// Import models
import { Respondent } from "../models/respondent";
import { Guid } from "guid-typescript";
import { LogService } from "./log.service";

@Injectable({
  providedIn: "root",
})
export class RespondentDataService {
  currentRespondent: Respondent;
  roleDataService: RoleDataService;

  constructor(
    private logService: LogService,
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string,
    private toastr: ToastrService
  ) { }

  getRespondents(): Observable<Respondent[]> {
    return this.http.get<Respondent[]>(this.baseUrl + "api/respondents").pipe(
      tap((_) => {
        this.logService.log("fetched respondents");
      }),
      catchError((err) => this.handleError("getRespondents", err))
    );
  }

  getRespondent(id: Guid): Observable<Respondent> {
    const url = `${this.baseUrl}api/respondents/${id.toString()}`;
    return this.http.get<Respondent>(url).pipe(
      tap((_) => {
        this.logService.log(`fetched respondent id=${id.toString()}`);
      }),
      catchError(this.handleError<Respondent>(`getRespondent id=${id}`))
    );
  }

  addRespondent(respondent: Respondent): Observable<Respondent> {
    this.currentRespondent = respondent;
    return this.http
      .post<Respondent>(this.baseUrl + "api/respondents", respondent)
      .pipe(
        tap((resp: Respondent) => {
          this.logService.log(`added respondent w/ id=${respondent.id.toString()}`);
        }),
        catchError(e => {
          this.handleError<Respondent>("addRespondent", e);
          throw e;
        })
      );
  }

  private handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
