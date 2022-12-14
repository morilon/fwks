
import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpClientModule } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { ToastrModule } from 'ngx-toastr'
import { ComponentsModule } from './components/components.module'
import { PipesModule } from './pipes/pipes.module'
import { ToastrSettings } from '@app/configs'
import { NgChartsModule } from 'ng2-charts'

@NgModule({
    imports: [
        HttpClientModule,
        ReactiveFormsModule,
        ToastrModule.forRoot(ToastrSettings.default),
        NgChartsModule,
        ComponentsModule
    ],
    exports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        ToastrModule,
        ComponentsModule,
        PipesModule
    ]
})
export class SharedModule { }
