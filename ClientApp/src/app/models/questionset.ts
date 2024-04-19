import { Question } from "./question";

export interface QuestionSet {
  id: number;
  questionSetName: string;
  introduction: string;
  versionNumber: string;
  fileSource: File;
  dateTimeCreated: Date;
  questions: Question[];
}

export interface SurveyQuestionSet {
  id: number;
  surveyId: number;
  questionSetId: number;
}
