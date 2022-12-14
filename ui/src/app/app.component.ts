import { Component, OnInit } from '@angular/core'
import { NgbPaginationConfig } from '@ng-bootstrap/ng-bootstrap'
import { PaginationSettings } from '@app/configs'
import { SessionService, StateService } from '@app/services'
import _fn from '@app/functions'
import { environment } from '@env'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent implements OnInit {

  envName: string

  constructor(
    public state: StateService,
    public session: SessionService,
    private pagination: NgbPaginationConfig
  ) {
    this.setDefaults()
    this.envName = environment.environment
  }

  ngOnInit(): void {
    document.title = 'Template UI'
  }

  setDefaults(): void {
    PaginationSettings.setDefault(this.pagination)
  }
}
