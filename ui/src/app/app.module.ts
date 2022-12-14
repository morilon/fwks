import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { KeycloakAngularModule } from 'keycloak-angular'
import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { PagesModule } from './pages/pages.module'
import _providers from './core/providers/_providers'

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    KeycloakAngularModule,
    PagesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
