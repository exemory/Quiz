import {Component, OnInit} from '@angular/core';
import {Test} from "../../interfaces/test";
import {HttpClient} from "@angular/common/http";
import {NotificationService} from "../../services/notification.service";
import {MatDialog} from "@angular/material/dialog";
import {StartTestDialog} from "../start-test-dialog/start-test-dialog.component";

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.scss']
})
export class TestsComponent implements OnInit {
  loading = true;
  loadingError = false;
  tests!: Test[];

  constructor(private api: HttpClient,
              private ns: NotificationService,
              private dialog: MatDialog) {
  }

  ngOnInit() {
    this.api.get<Test[]>('tests')
      .subscribe({
        next: tests => {
          this.tests = tests;
          this.loading = false;
        },
        error: err => {
          this.loading = false;
          this.loadingError = true;
          this.ns.notifyError(`Loading data failed. ${err.error?.message ?? ''}`);
        }
      })
  }

  onStartClick(test: Test) {
    const dialogRef = this.dialog.open(StartTestDialog,
      {
        maxWidth: '800px',
        width: '100%',
        autoFocus: false,
        data: test
      });
  }
}
