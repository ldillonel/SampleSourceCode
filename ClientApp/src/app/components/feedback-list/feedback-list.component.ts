import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Routes, RouterModule, Router } from "@angular/router";
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { DynamicTableComponent, ColumnConfig } from 'material-dynamic-table';
import { ToastrService } from "ngx-toastr";
import { saveAs } from 'file-saver';

//Import models and services
import { Feedback } from '../../models/feedback';
import { FeedbackDataService } from '../../services/feedback.data-service';
import { LogService } from 'src/app/services/log.service';


@Component({
  selector: 'app-feedback-list',
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.scss']
})
export class FeedbackListComponent {
  displayedColumns: string[] = ['select', 'survey', 'questionset', 'respondent', 'completed'];
  selection = new SelectionModel<Feedback>(true, []);
  dynamicTable: DynamicTableComponent;
  headers: Feedback = new Feedback();
  columns: ColumnConfig[] = [];
  data: Feedback[] = [];
  dataSource: MatTableDataSource<Feedback>;
  anonymous = false;

  constructor(
    private logService: LogService,
    private feedbackDataService: FeedbackDataService,
    private route: ActivatedRoute,
    private toastr: ToastrService) {
    this.route.params.subscribe(params => {
      if (params["anonymous"]) {
        this.anonymous = true;
      };
    });

    this.columns = Object.keys(this.headers).map((key) => {
      return {
        name: key,
        displayName: key.toUpperCase(),
        type: 'string'
      }
    });

    feedbackDataService.getData().subscribe(result => {
      this.data = result;
      this.dataSource = new MatTableDataSource<Feedback>(this.data);
    }, error => this.logService.log(error));
  }

  exportItems(): void {
    //this.selection.selected
    this.feedbackDataService.exportFeedbackArray(this.selection.selected).subscribe(result => {
      const file = new Blob([result as BlobPart], { type: 'text/csv;charset=utf-8' });
      saveAs(file, `SurveyResponses_${Date.now().toString()}.csv`);
      this.toastr.success("Your selected feedback has been exported.");
      this.selection.clear();
    });
  }


  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    if (this.dataSource) {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSource.data.length;
      return numSelected === numRows;
    }
    else
    {
      return false;
    }
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: Feedback): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

}
