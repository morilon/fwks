import { Component } from '@angular/core'
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html'
})
export class ConfirmationModalComponent {

  message: string
  confirmationLabel: string
  cancelLabel: string


  constructor(
    public modal: NgbActiveModal
  ) {
    this.message = 'Would you like to continue?'
    this.confirmationLabel = 'Yes'
    this.cancelLabel = 'No'
  }

}
