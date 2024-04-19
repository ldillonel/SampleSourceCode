import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class LogService {
  log(message: any) {
    // Log if not in production
    if (!environment.production) {
      console.log(new Date() + ": " + JSON.stringify(message));
    }
  }

  error(message: any) {
    console.error(new Date() + ": " + JSON.stringify(message));
  }
}
