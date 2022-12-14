import { Pipe, PipeTransform } from '@angular/core'

@Pipe({ name: 'gt' })
export class GreaterThanPipe implements PipeTransform {
    transform(data: any, value: number): boolean {
        if (!data) {
            return false
        }
        const length = typeof (data) === 'object' ? data.length : data.trim().length
        return length && length > value
    }
}
