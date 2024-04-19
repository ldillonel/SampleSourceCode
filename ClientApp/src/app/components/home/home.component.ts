import { Component, Inject } from '@angular/core';
import { SurveyDataService } from 'src/app/services/survey.data-service';
import { Survey } from 'src/app/models/survey';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ToastrService } from "ngx-toastr";
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  surveys: Survey[];
  accessCode: string;
  currentUser: User;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private surveyService: SurveyDataService,
    private authenticationService: AuthenticationService,
    private toastr: ToastrService,
    private router: Router) {
      this.authenticationService.currentUser.subscribe(
        x => (this.currentUser = x)
      );
  }

  ngOnInit(): void {
    // Refresh the list of current surveys - stores surveys to service member
    this.surveyService.getSurveys().subscribe(surveys => { this.surveys = surveys; });
  }

  submit() {
    let surveyId = this.surveyService.getSurveyIdByAccessCode(this.accessCode);

    // If it's a valid ID number
    if (surveyId && surveyId >= 0) {
      this.router.navigate(['/feedback/'+ surveyId, { anonymous: true }]);
    } else {
      console.error("InvalidAccessCode");
      this.showInvalidUniqueAccessCode();
    }
  }


  showInvalidUniqueAccessCode() {
    this.toastr.error("Invalid survey access code.");
  }

}
