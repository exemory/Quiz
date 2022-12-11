import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Question} from "../../interfaces/question";
import {HttpClient, HttpStatusCode} from "@angular/common/http";
import {NotificationService} from "../../services/notification.service";
import {CompletedTestData} from "../../interfaces/completed-test-data";
import {MatRadioChange} from "@angular/material/radio";
import {Answer} from "../../interfaces/answer";
import {TestResult} from "../../interfaces/test-result";

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss']
})
export class TestComponent implements OnInit {
  private testId!: string;
  loading = true;
  loadingError = false;

  questions!: Question[];
  currentQuestionIndex = 0;

  completedTestData: CompletedTestData = {
    completedQuestions: []
  }

  testResult?: TestResult;

  constructor(private route: ActivatedRoute,
              private api: HttpClient,
              private ns: NotificationService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.testId = params.get('testId')!;

      if (!this.testId.match(/^[\da-f]{8}-([\da-f]{4}-){3}[\da-f]{12}$/i)) {
        this.testDoesNotExists();
        return;
      }

      this.loadTestQuestions(this.testId);
    });
  }

  private testDoesNotExists() {
    this.ns.notifyError('Test does not exist.');
    this.router.navigate(['../']);
  }

  private loadTestQuestions(testId: string) {
    this.api.get<Question[]>(`questions?testId=${testId}`)
      .subscribe({
        next: questions => {
          this.questions = questions;
          this.loading = false;
        },
        error: err => {
          this.loading = false;
          this.loadingError = true;

          if (err.status === HttpStatusCode.NotFound) {
            this.testDoesNotExists();
            return;
          }

          this.ns.notifyError(`Loading data failed. ${err.error?.message ?? ''}`);
        }
      });
  }

  get isTestEmpty() {
    return this.questions.length === 0;
  }

  get currentQuestion() {
    return this.questions[this.currentQuestionIndex];
  }

  get isMoveToPrevDisabled() {
    return this.currentQuestionIndex <= 0;
  }

  get isMoveToNextDisabled() {
    return this.currentQuestionIndex >= this.questions.length - 1 || !this.currentQuestionAsCompleted;
  }

  onPrevClick() {
    if (this.currentQuestionIndex > 0) {
      this.currentQuestionIndex--;
    }
  }

  onNextClick() {
    if (this.currentQuestionIndex < this.questions.length - 1) {
      this.currentQuestionIndex++;
    }

    console.log(this.currentQuestionIndex)
    console.log(this.questions.length)
  }

  private get currentQuestionAsCompleted() {
    return this.completedTestData.completedQuestions
      .find(value => value.questionId === this.currentQuestion.id);
  }

  onAnswerChange(event: MatRadioChange) {
    let completedQuestion = this.currentQuestionAsCompleted;

    const answer = event.value as Answer;

    if (!completedQuestion) {
      completedQuestion = {
        questionId: this.currentQuestion.id,
        selectedAnswerId: answer.id
      }

      this.completedTestData.completedQuestions.push(completedQuestion);
      return;
    }

    completedQuestion.questionId = this.currentQuestion.id;
    completedQuestion.selectedAnswerId = answer.id;
  }

  get isAllQuestionsCompleted() {
    return this.questions.length === this.completedTestData.completedQuestions.length;
  }

  isAnswerChecked(answer: Answer) {
    let completedQuestion = this.currentQuestionAsCompleted;
    return completedQuestion?.selectedAnswerId === answer.id;
  }

  getTestResult() {
    if (!this.isAllQuestionsCompleted) {
      return;
    }

    this.api.post<TestResult>(`tests/${this.testId}/result`, this.completedTestData)
      .subscribe({
        next: testResult => {
          this.testResult = testResult;
        },
        error: err => {
          this.ns.notifyError(`Operation failed. ${err.error?.message ?? ''}`);
        }
      })
  }
}
