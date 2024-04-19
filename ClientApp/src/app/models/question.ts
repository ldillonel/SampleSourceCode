import { Response } from '../models/response';

export interface Question {
  id: number;
  questionSetId: number;
  questionTypeId: number;
  questionText: string;
  responseTypeId: number;
  dependencyQuestionId: number;
  ordinalPosition: number;
  min: number;
  max: number;
  page: number;
  pageOrder: number;
  response: Response[];
}
