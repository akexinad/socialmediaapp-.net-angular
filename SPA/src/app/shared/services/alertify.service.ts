import { Injectable } from '@angular/core';

// Because alertify was imported via the angular.json file, we do not import it in the generic way.
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallback();
      } else { }
    });
  }

  success(message: string) {
    alertify.success(message);
  }

  message(message: string) {
    alertify.message(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  error(message: string) {
    alertify.error(message);
  }

}
