import { SurveyQuestionSet } from "./questionset";

export interface Survey {
  id: number
  surveyAccessCode: string
  surveyTypeId: number
  surveyName: string
  description: string
  startDateTime: Date
  endDateTime: Date
  dateTimeCreated: Date
  surveyQuestionSet: SurveyQuestionSet[]
}
