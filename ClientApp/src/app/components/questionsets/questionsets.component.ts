import { Component, OnInit } from '@angular/core';
import { trigger, state, transition, style, animate } from "@angular/animations";

// Import models and services
import { QuestionSet } from '../../models/questionset'
import { QuestionSetDataService } from '../../services/questionset.data-service';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-questionsets',
  styleUrls: ['./questionsets.component.scss'],
  templateUrl: './questionsets.component.html',
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})

export class QuestionSetsComponent implements OnInit {
  columnsToDisplay = ['id', 'questionSetName', 'introduction', 'versionNumber', 'dateTimeCreated'];
  expandedElement: QuestionSet | null;
  public questionsets: QuestionSet[];
  cols: any[];

  constructor(
    private logService: LogService,
    private questionSetDataService: QuestionSetDataService) {
    this.questionSetDataService.getQuestionSets().subscribe(result => {
      this.questionsets = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {
    this.cols = [
      { field: "id", header: "ID"},
      { field: "questionSetName", header: "Name" },
      { field: "introduction", header: "Introduction" },
      { field: "versionNumber", header: "Version Number" },
      { field: "dateTimeCreated", header: "dateTimeCreated" }
    ];
  }

}

