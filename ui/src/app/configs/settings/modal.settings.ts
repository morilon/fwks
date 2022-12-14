import { NgbModalOptions } from '@ng-bootstrap/ng-bootstrap'

export class ModalSettings {

    static get default(): NgbModalOptions {
        return {
            centered: true,
            backdrop: 'static',
            keyboard: false,
            animation: true,
        }
    }

    static get small(): NgbModalOptions {
        const properties = this.default
        properties.size = 'sm'
        return properties
    }

    static get large(): NgbModalOptions {
        const properties = this.default
        properties.size = 'lg'
        return properties
    }

    static get xlarge():NgbModalOptions {
        const properties = this.default
        properties.size = 'xl'
        return properties
    }

    static get ultrawide(): NgbModalOptions {
        const properties = this.xlarge
        properties.windowClass += ' modal-dialog-ultrawide'
        return  properties
    }
}
