import { Pipe, PipeTransform } from '@angular/core'
import { ExtractPipeArgs } from '../args/common/extract.args'

@Pipe({ name: 'extract' })
export class ExtractPipe implements PipeTransform {
    transform(collection: any[], args: ExtractPipeArgs): string {
        return collection.find(x => x[args.id ?? 'id'] === args.key)[args.value]
    }
}
