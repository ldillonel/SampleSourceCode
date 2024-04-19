import { Component, OnInit, Input } from '@angular/core';
import { StepperFormService } from '../../services/stepper-form.service';
import { FormGroup, ControlContainer, NgForm } from '@angular/forms';
import { QuestionBase } from '../../models/question-base';
import { Step } from '../../models/step';
import { QuestionSet } from '../../models/questionset';

@Component({
  selector: 'app-stepper-form',
  templateUrl: './stepper-form.component.html',
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
  providers: [StepperFormService]
})
export class StepperFormComponent implements OnInit {
  @Input() feedbackForm: FormGroup;
  @Input() questionSets: QuestionSet[];
  @Input() respondentFields: QuestionBase<any>[];
  @Input() successToast;
  steps: Step[];

  constructor(private sfs: StepperFormService) { }

  ngOnInit() {
    this.steps = this.sfs.toSteps(this.questionSets);
  }

  isFormValid(): boolean {
    const mainForm = this.feedbackForm.controls['feedbackForm'] as FormGroup;
    const result = Object.values(mainForm.controls).every(item => item.valid);
    return result;
  }

}
