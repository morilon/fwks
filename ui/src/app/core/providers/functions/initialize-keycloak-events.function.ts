import { KeycloakEventType, KeycloakService } from 'keycloak-angular'
import { SessionService } from '@app/services'

export function initializeKeycloakEvents(
    keycloak: KeycloakService,
    session: SessionService) {
    return () => {
        keycloak.keycloakEvents$.subscribe({
            next: e => {
                switch (e.type) {
                    case KeycloakEventType.OnAuthSuccess:
                        session.setContext()
                        break
                    case KeycloakEventType.OnTokenExpired:
                        keycloak.updateToken()
                            .then(success => {
                                if (!success) {
                                    session.logout()
                                }
                            })
                            .catch(() => session.logout())
                        break
                }
            }
        })
    }
}
