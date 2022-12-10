import {Injectable} from '@angular/core';
import {MatSnackBar, MatSnackBarConfig} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private readonly defaultNotificationDuration = 2500;

  constructor(private snackBar: MatSnackBar) {
  }

  notify(message: string, messageType: 'success' | 'error', duration: number) {

    let config: MatSnackBarConfig = {
      duration,
      horizontalPosition: "right",
      verticalPosition: "top",
      panelClass: messageType
    }

    return this.snackBar.open(message, undefined, config);
  }

  notifySuccess(message: string) {
    return this.notify(message, 'success', this.defaultNotificationDuration);
  }

  notifyError(message: string) {
    return this.notify(message, 'error', this.defaultNotificationDuration);
  }
}
