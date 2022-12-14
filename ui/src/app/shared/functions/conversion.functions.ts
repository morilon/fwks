export class ConversionFunctions { 
    
    static toInt = (value: string): number =>
        value ? parseInt(value, 10) : 0

    static toFloat = (value: string): number =>
        value ? parseFloat(value) : 0

    static enumToList = (enumType: Record<string, unknown>, numbersOnly: boolean): any[] =>
        Object
            .keys(enumType)
            .filter(x => x.toLowerCase() !== 'undefined' && typeof enumType[x] === (numbersOnly ? 'number' : 'string'))
            .map(x => enumType[x])
}