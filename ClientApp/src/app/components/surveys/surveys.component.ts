import { Component, Inject, OnInit } from "@angular/core";
import { trigger, state, transition, style, animate } from "@angular/animations";

// Import models and services
import { Survey } from "../../models/survey";
import { SurveyDataService } from "../../services/survey.data-service";
import { LogService } from "src/app/services/log.service";

@Component({
  selector: "app-surveys",
  styleUrls: ['./surveys.component.scss'],
  templateUrl: "./surveys.component.html",
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class SurveysComponent implements OnInit {
  columnsToDisplay = ['surveyName', 'surveyAccessCode', 'startDateTime', 'endDateTime', 'dateTimeCreated', 'link'];
  expandedElement: Survey | null;
  public surveys: Survey[];
  cols: any[];

  constructor(
    private logService: LogService,
    private surveyService: SurveyDataService,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  ngOnInit(): void {
    this.surveyService.getSurveys().subscribe(surveys => { this.surveys = surveys; });

    this.cols = [
      { field: "surveyName", header: "Name"},
      { field: "surveyAccessCode", header: "Access Code"},
      { field: "startDateTime", header: "Start Date" },
      { field: "endDateTime", header: "End Date" },
      { field: "dateTimeCreated", header: "Created Date" },
      { field: "link", header: "Link" }
    ];
  }

  private surveyHasQuestionSets(rowClicked: Survey): boolean {
    return rowClicked.surveyQuestionSet.length > 0;
  }
}
