export const environment = {
    production: true,
    environment: 'qa',
    endpoints: {
        api: 'http://host.docker.internal:25001'
    },
    authServer: {
        url: 'http://host.docker.internal:10001/auth',
        realm: 'dev-service',
        clientId: 'dev-ui',
        scopes: 'openid profile email roles'
    }
}