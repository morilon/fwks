import { Injectable } from '@angular/core'
import { SessionService } from '..'

@Injectable({
    providedIn: 'root'
})
export class StateService {

    constructor(
        public session: SessionService
    ) {
    }
}
