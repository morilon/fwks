import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { AuthGuard } from '@app/guards'
import { HomeComponent, ProtectedComponent } from './pages/main'

const redirectRoute = '/home'

const routes: Routes = [
  { path: '', redirectTo: redirectRoute, pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'protected', component: ProtectedComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: redirectRoute, pathMatch: 'full' }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
