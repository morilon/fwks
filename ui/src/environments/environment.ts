// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.production.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  environment: 'dev',
  endpoints: {
    api: 'https://localhost:5001'
  },
  authServer: {
    url: 'http://localhost:10001/auth',
    realm: 'dev-service',
    clientId: 'dev-ui',
    scopes: 'openid profile email roles'
  }
}

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
