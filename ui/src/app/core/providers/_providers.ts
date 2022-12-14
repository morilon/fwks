import { APP_INITIALIZER } from '@angular/core'
import { KeycloakService } from 'keycloak-angular'
import { SessionService } from '@app/services'

import { initializeKeycloak } from './functions/initialize-keycloak.function'
import { initializeKeycloakEvents } from './functions/initialize-keycloak-events.function'

const _providers = [
    {
        provide: APP_INITIALIZER,
        useFactory: initializeKeycloak,
        multi: true,
        deps: [KeycloakService]
    },
    {
        provide: APP_INITIALIZER,
        useFactory: initializeKeycloakEvents,
        multi: true,
        deps: [KeycloakService, SessionService]
    }
]

export default _providers