import { NgbPaginationConfig } from '@ng-bootstrap/ng-bootstrap'

export class PaginationSettings {

    static setDefault(config: NgbPaginationConfig): void {
        config.pageSize = 12
        config.maxSize = 5
        config.boundaryLinks = true
        config.rotate = true
        config.ellipses = false
    }
}
