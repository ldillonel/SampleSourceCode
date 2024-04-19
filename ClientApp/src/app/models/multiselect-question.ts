import { QuestionBase } from './question-base';

export class MultiSelectQuestion extends QuestionBase<string> {
  controlType = 'multiselect';
  multiSelect: boolean;
  options: { key: string; value: string }[] = [];

  constructor(options: {} = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
