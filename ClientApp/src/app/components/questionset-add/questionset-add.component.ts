import { Component, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from "ngx-toastr";

// Import models and services
import { Survey } from '../../models/survey'
import { QuestionSetDataService } from '../../services/questionset.data-service';
import { SurveyDataService } from '../../services/survey.data-service';
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-questionset-add',
  templateUrl: './questionset-add.component.html',
  styleUrls: []
})
export class QuestionSetAddComponent {
  angForm: FormGroup;
  fileData: File = null;
  surveys: Survey[];

  constructor(
    private logService: LogService,
    private questionSetService: QuestionSetDataService,
    private surveyService: SurveyDataService,
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private toastr: ToastrService,
    private router: Router) {
    this.createForm();
    this.surveyService.getSurveys().subscribe(surveys => { this.surveys = surveys; }, error => this.logService.error(error));
  }

  fileSelected(fileInput: any) {
    this.fileData = fileInput.target.files[0] as File;
    const reader = new FileReader();

    if (fileInput.target.files && fileInput.target.files.length) {
      const [file] = fileInput.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
      };

      // need to run CD since file load runs outside of zone
      this.cd.markForCheck();
    };
  }

  createForm() {
    this.angForm = this.fb.group({
      id: ['', Validators.required],
      questionSetName: ['', Validators.required],
      surveyId: ['', Validators.required],
      introduction: ['', Validators.required],
      fileSource: ['', Validators.required],
      versionNumber: ['', Validators.required]
    });
  }
  submit() {
    const formData = new FormData();
    formData.append('questionSetName', this.angForm.get('questionSetName').value);
    formData.append('introduction', this.angForm.get('introduction').value);
    formData.append('versionNumber', this.angForm.get('versionNumber').value);
    formData.append('fileSource', this.fileData);

    this.questionSetService.postQuestionSet(formData).subscribe(result => {
      this.toastr.success("Your questionset was saved.");
    }, error => {
        this.logService.error(error);
        this.toastr.error("There was a problem saving your questionset.");
    });

    this.router.navigate(['/questionsets/', { anonymous: true }]);
  }

}
