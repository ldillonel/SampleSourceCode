import { Component, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";

//Import models and services
import { LogService } from "src/app/services/log.service";

@Component({
  templateUrl: "./feedback-landing.component.html"
})
export class FeedbackLandingComponent {
  anonymous = true;

  constructor(
    private logService: LogService,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}
}
