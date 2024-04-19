import { Injectable, Inject } from "@angular/core";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { catchError, tap, map, mergeMap, toArray } from "rxjs/operators";
import { Observable, of, merge, forkJoin } from "rxjs";
import { saveAs } from 'file-saver';

// Import models
import { Feedback } from "../models/feedback";
import { LogService } from "./log.service";
import { SurveyDataService } from './survey.data-service';
import { QuestionSetDataService } from './questionset.data-service';

let httpOptions = {
  headers: new HttpHeaders({
    "Content-Type": "application/json; charset=utf-8"
  })
};

@Injectable({
  providedIn: "root"
})
export class FeedbackDataService {
  dataServiceUrl: string;

  constructor(
    private logService: LogService,
    private httpClient: HttpClient,
    private surveyDataService: SurveyDataService,
    private questionsetDataService: QuestionSetDataService,
    @Inject("BASE_URL") private baseUrl: string
  ) {
    this.dataServiceUrl = `${baseUrl}api/Feedback`;
  }

  getFeedback(): Observable<Feedback[]> {
    return this.httpClient
      .get<Feedback[]>(this.dataServiceUrl, httpOptions)
      .pipe(
        tap(result => this.logService.log("fetched feedback items:" + result.length)),
        catchError(err => this.handleError("getFeedback", err))
      );
  }


  getData(): Observable<Feedback[]> {
    return this.getFeedback().pipe(mergeMap(items => items), mergeMap(item => {
      const svy = this.surveyDataService.getSurvey(item.surveyId);
      const qs = this.questionsetDataService.getQuestionSetById(item.questionSetId);
      return forkJoin([svy, qs]).pipe(map((result) => {
        const final = {...item, surveyName: result[0].surveyName, questionSetName: result[1].questionSetName } as Feedback
        return final;
      }))
    }), toArray());
  }


  addFeedback(feedback: Feedback) {
    return this.httpClient
      .post(this.dataServiceUrl, feedback, httpOptions)
      .pipe(
        tap(result => this.logService.log(result)),
        catchError(err => this.handleError("addFeedback", err))
      )
      .subscribe(val => this.logService.log("Feedback successfully added: " + val));
  }

  addFeedbackArray(feedback: Feedback[]) {
    return this.httpClient
      .post(
        `${this.dataServiceUrl}/PostFeedbackArray`,
        feedback,
        httpOptions
      )
      .pipe(
        tap(result => {
          this.logService.log("Feedback submitted: " + result);
        }),
        catchError(err => {
          return this.handleError<Feedback>("addFeedbackArray", err);
        })
      );
  }


  exportFeedbackArray(feedback: Feedback[]): Observable<Blob> {
    return this.httpClient
      .post(`${this.dataServiceUrl}/ExportFeedbackArray`,feedback, { responseType: 'blob' })
      .pipe(map(res => res as Blob));
  }

  private handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
