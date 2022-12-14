import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { FormsModule } from '@angular/forms'
import { PipesModule } from '../pipes/pipes.module'
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap'
import {
    PaginationComponent
} from '.'

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        NgbPaginationModule,
        PipesModule
    ],
    declarations: [
        PaginationComponent
    ],
    exports: [
        // modules
        CommonModule,
        FormsModule,
        PipesModule,
        // components
        PaginationComponent
    ]
})
export class ComponentsModule { }