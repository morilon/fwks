import { HttpClient } from '@angular/common/http'
import { Component } from '@angular/core'
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms'
import { environment } from '@env'
import { KeycloakService } from 'keycloak-angular'
import { KeycloakProfile } from 'keycloak-js'

@Component({
  selector: 'app-protected',
  templateUrl: './protected.component.html',
  styles: [
  ]
})
export class ProtectedComponent {

  user: KeycloakProfile
  customers: any
  error: any
  form: UntypedFormGroup
  fetch: boolean

  constructor(
    keycloak: KeycloakService,
    private http: HttpClient,
    private fb: UntypedFormBuilder
  ) {
    this.user = {}
    this.customers = []
    this.error = undefined
    this.form = fb.group({
      name: ['']
    })
    this.fetch = false
    keycloak.loadUserProfile().then((profile: KeycloakProfile) => this.user = profile)
  }

  searchCustomers(): void {
    const queryParam = this.form.controls.name.value == '' ? '' : `?name=${this.form.controls.name.value}`    
    this.http
      .get<any>(`${environment.endpoints.api}/v1/customers${queryParam}`).toPromise()
      .then(customers => {
        this.error = undefined
        this.customers = customers.data
        this.fetch = true
      })
      .catch(response => {
        this.error = response.error
        this.customers = {}
        this.fetch = false
      })
  }
}
