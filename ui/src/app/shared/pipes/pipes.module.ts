import { NgModule } from '@angular/core'

import {
    ExtractPipe,
    GreaterThanPipe,
    NotEmptyPipe,
    PaginatePipe
} from './'

@NgModule({
    declarations: [
        ExtractPipe,
        GreaterThanPipe,
        NotEmptyPipe,
        PaginatePipe,
    ],
    exports: [
        ExtractPipe,
        GreaterThanPipe,
        NotEmptyPipe,
        PaginatePipe
    ]
})
export class PipesModule { }