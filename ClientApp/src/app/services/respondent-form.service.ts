import { Injectable } from '@angular/core';

// Import models and services
import { SingleSelectQuestion } from '../models/singleselect-question';
import { QuestionBase } from '../models/question-base';
import { Respondent } from '../models/respondent';
import { Role } from '../models/role';
import { TextQuestion } from '../models/text-question';
import { Observable, of, forkJoin } from "rxjs";
import { catchError, tap, map } from "rxjs/operators";

import { RespondentDataService } from './respondent.data-service';
import { RoleDataService } from './role.data-service';
import { LogService } from './log.service';

@Injectable({
  providedIn: 'root'
})
export class RespondentFormService {
  //Create Respondent Fields
  repspondentsWithRoleData$ = forkJoin([
    this.roleDataService.getRoles(),
    of(Object.keys(new Respondent()).map(key => key))
  ]).pipe(map(([roles, respondentFields]) => 
    respondentFields.map(field => {
        switch (field) {
          case 'id':
            return new TextQuestion({
              key: `respondent${field}`,
              label: field.toUpperCase(),
              value: '0',
              type: 'text',
              required: false,
              readOnly: true
            })
          case 'roleId':
            return new SingleSelectQuestion({
              key: `respondent${field}`,
              label: "ROLE",
              multiSelect: false,
              options: Object.entries(roles).map(([, value]) => { return { key: value.id, value: value.roleName } }),
              required: true,
            })
          default:
            return new TextQuestion({
              key: `respondent${field}`,
              label: field.toUpperCase(),
              type: 'text',
              required: true,
              readOnly: false
            })
        }
    }) as QuestionBase<any>[]
  ), tap(() => this.logService.log("Getting Respondent Fields")),
      catchError(err => {
        this.logService.log(err);
        throw err;
      })
  );

  constructor(private roleDataService: RoleDataService, private respondentDataService: RespondentDataService, private logService: LogService) {
  }

}
