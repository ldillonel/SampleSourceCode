import { QuestionBase } from './question-base';

export class RatingQuestion extends QuestionBase<number> {
  controlType = 'rating';
  options: { key: string; value: string }[] = [];

  constructor(options: {} = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
