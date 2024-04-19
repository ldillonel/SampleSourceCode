import { ResultResponse } from "./result-response";
import { Guid } from "guid-typescript";

export class Feedback {
  id?: string;
  surveyId?: number;
  questionSetId?: number;
  contactTypeId?: number;
  respondentId?: string;
  dateTimeCreated?: Date;
  timeCompleted?: Date;
  resultResponse?: ResultResponse[];

  constructor() { 
  this.id = undefined;
  this.surveyId = undefined;
  this.questionSetId = undefined;
  this.contactTypeId = undefined;
  this.respondentId = undefined;
  this.dateTimeCreated = undefined;
  this.timeCompleted = undefined;
  }
}
