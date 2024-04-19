import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

//Import modles
import { QuestionBase } from '../models/question-base';

@Injectable({
  providedIn: 'root'
})
export class DynamicQuestionService {
  toFormGroup(questions: QuestionBase<any>[]) {
    const config: any = {};
    questions.forEach(question => {
      config[question.key] = question.required ? new FormControl(question.value || '', Validators.required)
        : new FormControl(question.value || '');
    });
    return new FormGroup(config);
  }
}
