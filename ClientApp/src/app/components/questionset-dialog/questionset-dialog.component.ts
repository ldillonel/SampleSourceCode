import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

// Import models and services
import { QuestionSetDataService } from '../../services/questionset.data-service';
import { QuestionSet } from '../../models/questionset';
import { ToastrService } from "ngx-toastr";
import { LogService } from 'src/app/services/log.service';

@Component({
  selector: 'questionsetdialog',
  templateUrl: './questionset-dialog.component.html',
  styleUrls: []
})
export class QuestionSetDialogComponent implements OnInit {
  angForm: FormGroup;
  fileData: File = null;
  surveyId: string;
  surveys = [];

  constructor(
    private logService: LogService,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private questionSetService: QuestionSetDataService,
    @Inject('BASE_URL') private baseUrl: string,
    private dialogRef: MatDialogRef<QuestionSetDialogComponent>,
    private cd: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) data) {
    this.surveyId = '1';
  }

  fileSelected(fileInput: any) {
    this.fileData = <File>fileInput.target.files[0];
    let reader = new FileReader();

    if (fileInput.target.files && fileInput.target.files.length) {
      const [file] = fileInput.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.angForm.patchValue({
          fileSource: reader.result
        });

        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }

  ngOnInit() {
    this.angForm = this.fb.group({
      id: ['', Validators.required],
      surveyId: ['', Validators.required],
      introduction: ['', Validators.required],
      questionSetName: ['', Validators.required],
      fileSource: ['', Validators.required],
      versionNumber: ['', Validators.required]
    });
  }

  save() {
    const formData = new FormData();

    formData.append('introduction', this.angForm.get('introduction').value);
    formData.append('questionSetName', this.angForm.get('questionSetName').value);
    formData.append('versionNumber', this.angForm.get('versionNumber').value);
    formData.append('surveyId', this.surveyId);
    formData.append('fileSource', this.fileData);

    // Call the service, do not navigate to QS page so we can finish creating survey
    this.questionSetService.postQuestionSet(formData).subscribe((item: QuestionSet) => {
      this.logService.log('done creating: ' + item);
      this.angForm.setValue({ id: item.id, surveyId: this.surveyId, questionSetName: item.questionSetName, introduction: item.introduction, fileSource: this.fileData, versionNumber: item.versionNumber })
      this.showSuccess();
      this.dialogRef.close(this.angForm.value);
    }, error => this.showError(error));
  }

  close() {
    this.dialogRef.close();
  }

  showSuccess() {
    this.toastr.success(
      "Your questionset was added successfully.",
      "Add QuestionSet"
    );
  }

  showError(error: string) {
    this.logService.error(error);
    this.toastr.error("There was a problem adding your questionset.",
      "Add QuestionSet");
  }
}

