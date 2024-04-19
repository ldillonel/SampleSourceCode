import { Guid } from "guid-typescript";

export interface ResultResponse {
  id?: number,
  feedbackId?: Guid,
  questionId: number,
  responseId?: number,
  responseText?: string,
  versionNumber: string,
  dateTimeCreated: Date
}
