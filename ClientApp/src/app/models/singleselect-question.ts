import { QuestionBase } from './question-base';

export class SingleSelectQuestion extends QuestionBase<string> {
  controlType = 'singleselect';
  multiSelect: boolean;
  options: { key: string; value: string }[] = [];

  constructor(options: {} = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
