import { QuestionBase } from './question-base';

export class UploadQuestion extends QuestionBase<string> {
  controlType = 'upload';
  type: 'file';

  constructor(options: {} = {}) {
    super(options);
    this.type = options['type'] || '';
  }
}
