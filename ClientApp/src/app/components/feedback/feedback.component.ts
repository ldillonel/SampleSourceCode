import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';

// Import models
import { Survey } from '../../models/survey';
import { QuestionSet } from '../../models/questionset';
import { Feedback } from '../../models/feedback';
import { Question } from '../../models//question';
import { Step } from '../../models/step';
import { Respondent } from '../../models/respondent';
import { QuestionBase } from '../../models/question-base';
import { ResultResponse } from '../../models/result-response';

// Import services
import { FeedbackDataService } from '../../services/feedback.data-service';
import { QuestionSetDataService } from '../../services/questionset.data-service';
import { RespondentDataService } from '../../services/respondent.data-service';
import { RespondentFormService } from '../../services/respondent-form.service';
import { SurveyDataService } from '../../services/survey.data-service';
import { Guid } from 'guid-typescript';
import { ToastrService } from "ngx-toastr";
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss'],
  providers: [RespondentFormService]
})
/** feedback component*/
export class FeedbackComponent {
  survey: Survey;
  questionSets: QuestionSet[];
  questions: Question[];
  feedback: Feedback[];
  feedbackForm: FormGroup;
  submitted: boolean;
  steps: Step[] = [];
  respondent: Respondent = new Respondent();
  respondentFields: QuestionBase<any>[];
  // Create a unique guid for this feedback form submission
  feedbackGUID: Guid;

  get formQuestions() {
    return this.feedbackForm.get('questions') as FormArray;
  }

  get formRespondent() {
    return this.feedbackForm.get('respondent') as FormArray;
  }


  /** feedback actor */
  constructor(
    private toastr: ToastrService,
    private logService: LogService,
    private feedbackService: FeedbackDataService,
    private surveyService: SurveyDataService,
    private questionSetService: QuestionSetDataService,
    private respondentService: RespondentDataService,
    private respondentFormService: RespondentFormService,
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute,
    private router: Router) {
    this.questions = [];

    // Generate a new GUID
    this.feedbackGUID = Guid.create();

    // Create the main form
    this.feedbackForm = new FormGroup({ feedbackForm: new FormGroup({}) });

    // Create respondent fields
    this.respondentFormService.repspondentsWithRoleData$.subscribe(results => this.respondentFields = results, err => this.logService.log(err));

    //Get related QuestionSets
    this.currentRoute.paramMap.subscribe(p => {
      const surveyId = +p.get("surveyId");
      this.surveyService.getSurvey(surveyId).subscribe(survey => this.survey = survey, error => console.error(error));
      this.questionSetService.getQuestionSetsBySurveyId(surveyId).subscribe(qs => {
        this.questionSets = qs;
      }, error => this.logService.log(error));
    });
  }

  onSubmit() {
    if (!this.feedbackForm.valid) {
      return;
    }

    const formData = this.feedbackForm.controls['feedbackForm'] as FormGroup;

    // Force set to 0 for new respondent
    this.respondent['id'] = this.feedbackGUID.toString();
    this.respondent['roleId'] = 1 //Hard-coding Test Role to minimize code changes.

    //save respondent & feedback
    this.respondentService.addRespondent(this.respondent).subscribe(r => {
      this.respondent = r;
      this.feedback = this.mapFeedbackFormToFeedback(formData);
      this.feedbackService.addFeedbackArray(this.feedback).subscribe(result => this.showSuccess(),
        error => this.showError(error));
    });
    this.submitted = true;
    this.router.navigate(['/feedback-landing/']);
  }

  private mapFeedbackFormToFeedback(feedbackForm: FormGroup): Feedback[] {
    const mappedFeedbackArray = [];
    // Create a unique guid for this feedback form submission
    const feedbackGUID = Guid.create();
    const respondentGUID = this.respondentService.currentRespondent.id;

    this.questionSets.forEach(qs => {
      const mappedFeedbackObj = {
        surveyId: this.survey.id,
        questionSetId: qs.id,
        feedbackId: feedbackGUID.toString(),
        contactTypeId: 1, //Email. Hard-coding for now.
        respondentId: respondentGUID.toString(), //returned from service.
        dateTimeCreated: new Date(),
        timeCompleted: new Date(),
        resultResponse: []
      }

      qs.questions.forEach(q => {
        switch (q.responseTypeId) {
          //SingleSelect
          case 1:
            mappedFeedbackObj.resultResponse.push({
              questionId: q.id,
              responseId: feedbackForm.get(`qs${q.questionSetId}_question${q.id}`).value,
              dateTimeCreated: new Date(),
              versionNumber: "0.1.0"
            });
            break;

          //MultiSelect
          case 2:
            {
              const responses = feedbackForm.get(`qs${q.questionSetId}_question${q.id}`).value;

              responses.forEach(r => {
                mappedFeedbackObj.resultResponse.push({
                  questionId: q.id,
                  responseId: r as number,
                  dateTimeCreated: new Date(),
                  versionNumber: "0.1.0"
                })
              });
              break;
            }
          //Binary
          case 3:
            {
              const responseValue = feedbackForm.get(`qs${q.questionSetId}_question${q.id}`).value;
              const resultResponse: ResultResponse = {
                questionId: q.id,
                dateTimeCreated: new Date(),
                versionNumber: "0.1.0",
                responseText: responseValue
              }

              mappedFeedbackObj.resultResponse.push(resultResponse);
              break;
            }
          //Scale
          case 4:
            mappedFeedbackObj.resultResponse.push({
              questionId: q.id,
              responseText: feedbackForm.get(`qs${q.questionSetId}_question${q.id}`).value,
              dateTimeCreated: new Date(),
              versionNumber: "0.1.0"
            });
            break;

          //All Others
          default:
            mappedFeedbackObj.resultResponse.push({
              questionId: q.id,
              responseText: feedbackForm.get(`qs${q.questionSetId}_question${q.id}`).value,
              dateTimeCreated: new Date(),
              versionNumber: "0.1.0"
            });
            break;
        }
      }); //end of questions loop.

      mappedFeedbackArray.push(mappedFeedbackObj);
    }); //end of question set loop.

    return mappedFeedbackArray;
  }

  showSuccess() {
    this.toastr.success(
      "Your feedback was submitted successfully!",
      "Submit Feedback"
    );
  }

  showError(error: string) {
    this.logService.error(error);
    this.toastr.error("An error occurred when attempting to submit the feedback.",
      "Submit Feedback");
  }


}
