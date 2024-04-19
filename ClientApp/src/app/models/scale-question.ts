import { QuestionBase } from './question-base';

export class ScaleQuestion extends QuestionBase<string> {
  controlType = 'scale';
  options: { key: string; value: string }[] = [];

  constructor(options: {} = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
