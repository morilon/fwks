import { GlobalConfig } from 'ngx-toastr'

export class ToastrSettings {

    static get default(): Partial<GlobalConfig> {
        return {
            timeOut: 2500
        }
    }

    static get long(): Partial<GlobalConfig> {
        return {
            timeOut: 5000
        }
    }
}
