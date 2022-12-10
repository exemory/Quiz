import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {Test} from "../../interfaces/test";
import {Router} from "@angular/router";

@Component({
  selector: 'app-test-description-dialog',
  templateUrl: './start-test-dialog.component.html',
  styleUrls: ['./start-test-dialog.component.scss']
})
export class StartTestDialog {

  agreeToStart = false;

  constructor(@Inject(MAT_DIALOG_DATA) public test: Test,
              private dialogRef: MatDialogRef<StartTestDialog>,
              private router: Router) {
  }

  onProceedClick() {
    this.router.navigate([this.test.id]);
    this.dialogRef.close();
  }
}
