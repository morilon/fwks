import { Pipe, PipeTransform } from '@angular/core'

@Pipe({ name: 'notEmpty' })
export class NotEmptyPipe implements PipeTransform {
    transform(data: any): boolean {
        return typeof (data) === 'object' ? data.length > 0 : data.trim().length > 0
    }
}
