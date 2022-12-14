import { NgModule } from '@angular/core'
import { SharedModule } from '../shared/shared.module'
import { HomeComponent, ProtectedComponent } from './main'

@NgModule({
    imports: [
        SharedModule
    ],
    declarations: [
        // main
        HomeComponent,
        ProtectedComponent
    ],
    exports: [
    ]
})
export class PagesModule { }