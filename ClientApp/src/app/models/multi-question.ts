import { QuestionBase } from './question-base';

export class MultiQuestion extends QuestionBase<string> {
  controlType = 'multichoice';
  options: { key: string; value: string }[] = [];

  constructor(options: {} = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
