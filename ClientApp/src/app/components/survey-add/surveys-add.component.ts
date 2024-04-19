import { Component, Inject, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MatListOption } from '@angular/material/list';

// Import components, models and services

import { QuestionSet } from '../../models/questionset';
import { QuestionSetDialogComponent } from '../questionset-dialog/questionset-dialog.component';
import { Survey } from '../../models/survey';
import { SurveyType } from '../../models/surveytype';
import { QuestionSetDataService } from '../../services/questionset.data-service';
import { SurveyDataService } from '../../services/survey.data-service';
import { ToastrService } from "ngx-toastr";
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'app-survey-add',
  templateUrl: './surveys-add.component.html',
  styleUrls: []
})
export class SurveyAddComponent {
  angForm: FormGroup;
  surveyTypes: SurveyType[] = [];
  questionSets = [];
  surveyCodes = [];
  fileData: File = null;
  selectedOptions: QuestionSet[] = [];
  selectedOption;

  constructor(
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private surveyService: SurveyDataService,
    private questionSetService: QuestionSetDataService,
    private logService: LogService,
    private toastr: ToastrService,
    private fb: FormBuilder, private cd: ChangeDetectorRef,
    private dialog: MatDialog) {
    this.getSurveyTypes();
    this.getSurveyCodes();
    this.getQuestionSets();
    this.createForm();
  }

  openDialog() {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.autoFocus = true;

    this.dialog.open(QuestionSetDialogComponent, dialogConfig)
      .afterClosed().subscribe(result => {
        if (result !== undefined) {
          this.questionSets.push(result);
          this.cd.detectChanges();
        }
      });
  }

  getSurveyTypes() {
    this.surveyService.getSurveyTypes().subscribe(result => {
      this.surveyTypes = result;
    }, error => {
        this.logService.error(error);
    });
  }

  getSurveyCodes() {
    this.surveyService.getSurveyCodes().subscribe(result => {
      this.surveyCodes = result;
    }, error => {
        this.logService.error(error);
    });
  }

  getQuestionSets() {
    this.questionSetService.getQuestionSets().subscribe(result => {
      this.questionSets = result;
    }, error => {
        this.logService.error(error);
    });
  }

  createForm() {
    this.angForm = this.fb.group({
      surveyTypeId: ['', Validators.required],
      surveyAccessCode: ['', [Validators.required, this.isSurveyAccessCodeUnique()]],
      description: ['', Validators.required],
      startDateTime: ['', Validators.required],
      endDateTime: ['', Validators.required],
      questionSet: ['', Validators.required],
    });
  }

  fileSelected(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];

    let reader = new FileReader();
    if (fileInput.target.files && fileInput.target.files.length) {
      const [file] = fileInput.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }

  onQuestionSetChanged(options: MatListOption[]) {
    this.selectedOptions = options.map(o => o.value);
  }

  submit() {
      const newSurvey: Survey = {
        surveyTypeId: this.angForm.get('surveyTypeId').value,
        surveyAccessCode: this.angForm.get('surveyAccessCode').value,
        surveyName: this.angForm.get('description').value,
        //setting description field to avoid NOT NULL-able constraint in database.
        description: this.angForm.get('description').value,
        startDateTime: this.angForm.get('startDateTime').value,
        endDateTime: this.angForm.get('endDateTime').value,
        surveyQuestionSet: this.selectedOptions.map(qs => { return { questionSetId: qs.id } })
      } as Survey;
      const options = {
        indices: true,
      };

    this.surveyService.addSurvey(newSurvey, options).subscribe(result => {
      this.showSuccess();
      this.router.navigate(["/surveys/"]);
    }, error => {
        this.showError(error);
        this.router.navigate(["/surveys/"]);
    });
  }

  isSurveyAccessCodeUnique(): ValidatorFn {
    return (c: AbstractControl): { [key: string]: boolean } | null => {
      const hasDuplicate = this.surveyCodes.indexOf(c.value) !== -1;
      if (hasDuplicate) {
        return { duplicate: true };
      }
      return null;
    }
  }


  showSuccess() {
    this.toastr.success(
      "Your survey was created successfully.",
      "Create Survey"
    );
  }

  showError(error: string) {
    this.logService.error(error);
    this.toastr.error("There was a error creating your survey.",
      "Create Survey");
  }
}
