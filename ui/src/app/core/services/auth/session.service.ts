import { Injectable } from '@angular/core'
import { KeycloakService } from 'keycloak-angular'

@Injectable({
    providedIn: 'root'
})
export class SessionService {
    isLoggedIn: boolean
    ready: boolean
    constructor(
        private keycloak: KeycloakService
    ) {
        this.isLoggedIn = false
        this.ready = false
    }

    login(): void {
        this.keycloak.login()
    }

    logout(): void {
        this.clearContext()
        this.keycloak.logout(`${window.location.origin}`)
    }

    setContext(): void {
        this.isLoggedIn = true
    }

    clearContext(): void {
        this.isLoggedIn = false
    }
}
