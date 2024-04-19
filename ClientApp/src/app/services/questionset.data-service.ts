import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, of, throwError } from "rxjs";
import { tap, catchError } from "rxjs/operators";

// Import models
import { QuestionSet } from "../models/questionset";
import { LogService } from "./log.service";
import { ToastrService } from "ngx-toastr";

const httpOptions = {
  headers: new HttpHeaders({
    "Content-Type": "application/json; charset=utf-8"
  })
};

@Injectable({
  providedIn: "root"
})
export class QuestionSetDataService {
  dataServiceUrl: string;

  constructor(
    private logService: LogService,
    private httpClient: HttpClient,
    @Inject("BASE_URL") private baseUrl: string) {
    this.dataServiceUrl = `${baseUrl}api/QuestionSets`;
  }

  getQuestionSets(): Observable<QuestionSet[]> {
    return this.httpClient
      .get<QuestionSet[]>(this.dataServiceUrl, httpOptions)
      .pipe(tap(qs => {
        this.logService.log("Retrieved QuestionSet Count: " + qs.length);
      }),
        catchError(err =>
          this.handleError("Error while attempting retrieve QuestionSets.", err)));
  }


  getQuestionSetById(id: number): Observable<QuestionSet> {
    return this.httpClient
      .get<QuestionSet>(`${this.dataServiceUrl}/${id}`)
      .pipe(tap(qs => {
        this.logService.log("Retrieved QuestionSet: " + qs.id);
      }),
        catchError(err =>
          this.handleError("Error while attempting retrieve QuestionSets.", err)));
  }



  postQuestionSet(formData) {
    return this.httpClient.post(this.dataServiceUrl, formData).pipe(
      tap(result => {
        this.logService.log("Added new Question Set: " + result);
      }),
      catchError(err => {
        this.logService.log(`Error occurred during QuestionSet POST: ${err}`);
        return this.handleError("Error while attempting retrieve QuestionSets.", err);
      })
    );
  }

  getQuestionSetsBySurveyId(surveyId: number): Observable<QuestionSet[]> {
    return this.httpClient.get<QuestionSet[]>(
      `${this.dataServiceUrl}/GetQuestionSetsBySurveyId/${surveyId}`, httpOptions)
      .pipe(tap(result => {
        this.logService.log("Retrieved QuestionSet(s) from Survey ID: " + surveyId);
        this.logService.log("Retrieved QuestionSet(s) result: " + result);
      }),
        catchError(err =>
          this.handleError<QuestionSet[]>("Error when getting QuestionSets by Survey Id.", err))
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
