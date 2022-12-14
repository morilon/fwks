import { UntypedFormArray } from '@angular/forms'

export class LinqFunctions { 
    
    static mapFormArray = <T>(formArray: UntypedFormArray, source: any[], key: string): T[] =>
        formArray.value.map((v: string, i: number) => v ? source[i][key] : undefined).filter((x: T) => x)

    static sortBy = <T>(key: keyof T, decending: boolean = false): (a: T, b: T) => number => {
            const order = decending ? -1 : 1
            return (a, b): number => {
                const type = typeof a[key]
                const valA = a[key] as any
                const valB = b[key] as any
                if (type === 'string') {
                    return order === 1
                        ? valA.toString().localeCompare(valB.toString())
                        : valB.toString().localeCompare(valA.toString())
                }
                if (valA < valB) {
                    return -order
                }
                if (valA > valB) {
                    return order
                }
                return 0
            }
        }
}