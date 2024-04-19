import { Feedback } from "./feedback";
import { Guid } from "guid-typescript";

export class Respondent {
  id?: string;
  mos: string;
  firstName: string;
  lastName: string;
  email: string;
  roleId: number;
  feedbacks: Feedback[];

  constructor() {
    this.id = Guid.create().toString();
    this.mos = "";
    this.firstName = "";
    this.lastName = "";
    this.roleId = 0;
    this.email = "";
  }
}

export interface RespondentField {
  key: string;
  type: string;
}
