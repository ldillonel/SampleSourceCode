import { Injectable } from '@angular/core';

// Import models and services
import { Step } from '../models/step';
import { QuestionSet } from '../models/questionset';
import { SingleSelectQuestion } from '../models/singleselect-question';
import { MultiSelectQuestion } from '../models/multiselect-question';
import { BinaryQuestion } from '../models/binary-question';
import { ScaleQuestion } from '../models/scale-question';
import { RatingQuestion } from '../models/rating-question';
import { TextQuestion } from '../models/text-question';
import { MemoQuestion } from '../models/memo-question';
import { Response } from '../models/response';

@Injectable({
  providedIn: 'root'
})
export class StepperFormService {
  toSteps(questionSets: QuestionSet[]): Step[] {
    const items = questionSets.map(item => {
      return {
        label: item.questionSetName,
        questions: item.questions.map(q => {
          switch (q.responseTypeId) {
            case 1: {
              return new SingleSelectQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                required: true,
                options: q.response.map((r: Response) => {
                  return { key: r.id, value: r.responseText }
                })
              });
            }
            case 2: {
              return new MultiSelectQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                required: true,
                options: q.response.map((r: Response) => {
                  return { key: r.id, value: r.responseText }
                })
              })
            }
            case 3: {
              return new BinaryQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                type: 'radio',
                required: true,
                options: [{ key: false, value: 'No' }, { key: true, value: 'Yes' }]
              })
            }
            case 4: {
              return new ScaleQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                type: 'scale',
                required: true,
                options: [
                  { key: q.min, value: 'Strongly Disagree' },
                  { key: q.min + 1, value: 'Disagree' },
                  { key: q.min + 2, value: 'Agree' },
                  { key: q.min + 3, value: 'Strongly Agree' }
                ]
              })
            }
            case 5: {
              const ratings = [...Array(q.max).keys()];
              return new RatingQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                type: 'rating',
                required: true,
                options: Object.keys(ratings).map((key) => { return { key: key, value: key } })
              })
            }
            case 6: {
              return new TextQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                type: 'text',
                required: true
              })
            }
            case 7: {
              return new MemoQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                type: 'textarea',
                required: true
              })
            }
            default:
              return new TextQuestion({
                key: `qs${q.questionSetId}_question${q.id}`,
                label: q.questionText,
                type: 'text',
                required: false
              })
          }
        })
      }
    });
    return items;
  }

  constructor() {

  }


}
