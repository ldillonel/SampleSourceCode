import { Component, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

// Import models
import { QuestionBase } from '../../models/question-base';

@Component({
  selector: 'app-dynamic-question-form',
  templateUrl: './dynamic-question-form.component.html',
  styleUrls: ['./dynamic-question-form.component.scss']
})

export class DynamicQuestionFormComponent {
  @Input() question: QuestionBase<any>;
  @Input() formGroup: FormGroup;
  private rating: number;

  constructor(fb: FormBuilder) {
    this.formGroup = fb.group({
      title: fb.control('initial value', Validators.required)
    });
  }

  onClick(rating: number) {
    //update rating to reflect value change in UI.
    this.rating = rating;

    //update form control's value to make sure it gets posted back.
    this.formGroup.get(this.question.key).setValue(rating);
    return false;
  }
  showIcon(index: number) {
    if (this.rating >= index + 1) {
      return 'star';
    } else {
      return 'star_outline';
    }
  }
  get isValid() { return this.formGroup.controls[this.question.key].valid; }
}

