import { Inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders} from "@angular/common/http";
import { Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { objectToFormData } from "object-to-formdata";

// Import models
import { Survey } from "../models/survey";
import { SurveyType } from "../models/surveytype";
import { LogService } from "./log.service";

const httpOptions = {
  headers: new HttpHeaders({ "Content-Type": "application/json" })
};

@Injectable({
  providedIn: "root"
})
export class SurveyDataService {
  public Surveys: Survey[] = [];

  constructor(
    private http: HttpClient,
    private router: Router,
    private logService: LogService,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  getSurveyTypes(): Observable<SurveyType[]> {
    return this.http
      .get<SurveyType[]>(this.baseUrl + "api/surveytypes", httpOptions)
      .pipe(
        tap(product => this.logService.log("fetched survey types")),
        catchError(err => this.handleError("getSurveyTypes", err))
      );
  }

  getSurveyCodes(): Observable<string[]> {
    const url = `${this.baseUrl}api/surveys/getSurveyCodes`;
    return this.http
      .get<string[]>(url, httpOptions)
      .pipe(
        tap(codes => this.logService.log("fetched survey codes")),
        catchError(err => this.handleError("getSurveyCodes", err))
      );
  }

  getSurveys(): Observable<Survey[]> {
    return this.http
      .get<Survey[]>(this.baseUrl + "api/surveys", httpOptions)
      .pipe(
        tap(product => {
          this.logService.log("fetched surveys count:" + product.length);
          this.Surveys = product;
        }),
        catchError(err => this.handleError("getSurveys", err))
      );
  }

  getSurvey(id: number): Observable<Survey> {
    const url = `${this.baseUrl}api/surveys/${id}`;
    return this.http.get<Survey>(url, httpOptions).pipe(
      tap(_ => this.logService.log(`fetched survey id=${id}`)),
      catchError(this.handleError<Survey>(`getSurvey id=${id}`))
    );
  }

  getSurveyIdByAccessCode(surveyAccessCode: string): number {
    let surveyId = -1;

    this.Surveys.forEach(element => {
      if (element.surveyAccessCode == surveyAccessCode) {
        surveyId = element.id;
      }
    });

    return surveyId;
  }

  addSurvey(survey: Survey, options?: any): Observable<Survey> {
    if (options == undefined) {
      return this.http
        .post<Survey>(this.baseUrl + "surveys", survey, httpOptions)
        .pipe(
          tap((surv: Survey) => {
            this.logService.log(`added survey w/ id=${survey.id}`);
          }),
          catchError(this.handleError<Survey>("addSurvey"))
        );
    } else {
      return this.http
        .post(this.baseUrl + "api/surveys", objectToFormData(survey, options))
        .pipe(
          tap((result: Survey) => {
            this.logService.log("added survey w/out id result: " + result);
          }),
          catchError(this.handleError<Survey>("addSurvey"))
        );
    }
  }

  updateSurvey(id: any, survey: Survey): Observable<any> {
    const url = `${this.baseUrl} + 'surveys'/${id}`;
    return this.http.put(url, survey, httpOptions).pipe(
      tap(_ => this.logService.log(`updated survey id=${id}`)),
      catchError(this.handleError<any>("updateSurvey"))
    );
  }

  deleteSurvey(id: any): Observable<Survey> {
    const url = `${this.baseUrl} + 'surveys'/${id}`;
    return this.http.delete<Survey>(url, httpOptions).pipe(
      tap(_ => this.logService.log(`deleted survey id=${id}`)),
      catchError(this.handleError<Survey>("deleteSurvey"))
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
