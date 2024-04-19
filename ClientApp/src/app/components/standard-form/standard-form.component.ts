import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';

// Import models and services
import { QuestionBase } from '../../models/question-base';
import { DynamicQuestionService } from '../../services/dynamic-question.service';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-standard-form',
  templateUrl: './standard-form.component.html',
  styleUrls: ['./standard-form.component.scss'],
  providers: [DynamicQuestionService]
})
export class StandardFormComponent implements OnInit {
  @Input() questions: QuestionBase<string>[];
  @Input() parentFormGroup: FormGroup;
  @Input() enableNumbering: boolean;
  @Input() successToast;
  formGroup: FormGroup;
  payLoad = '';

  constructor(
    private logService: LogService,
    private qcs: DynamicQuestionService) { }

  ngOnInit() {

    const fb: FormBuilder = new FormBuilder();
    this.formGroup = this.qcs.toFormGroup(this.questions);
    const parent = this.parentFormGroup.controls['feedbackForm'] as FormGroup;
    const fg: FormGroup = fb.group({
      ...parent.controls,
      ...this.formGroup.controls
    });
    parent.controls = fg.controls;
  }
}
