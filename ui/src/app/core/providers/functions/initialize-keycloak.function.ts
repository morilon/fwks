import { KeycloakService } from 'keycloak-angular'
import { environment } from '@env'

export function initializeKeycloak(
    keycloak: KeycloakService) {
    return () =>
        keycloak.init({
            config: {
                url: environment.authServer.url,
                realm: environment.authServer.realm,
                clientId: environment.authServer.clientId,

            },
            initOptions: {
                pkceMethod: 'S256',
                onLoad: 'check-sso',
                silentCheckSsoRedirectUri: window.location.origin + '/assets/silent-check-sso.html'
            },
            enableBearerInterceptor: true,
            loadUserProfileAtStartUp: true,
            bearerExcludedUrls: ['assets']
        })
}
