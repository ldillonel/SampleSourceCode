import { QuestionBase } from "./question-base";

export class Step {
  label: string;
  questions: QuestionBase<any>[];
}
