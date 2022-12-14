import { RegexPatterns } from '@app/configs'

export class CasingFunctions {

    static spaceCamelCase = (value: string): string =>
        value.replace(RegexPatterns.spaceCamel, ' $1').trim()

    static toCamelCase = (value: string): string =>
        value.replace(RegexPatterns.camelCase, (_, ch: string) => ch.toUpperCase())

    static toKebabCase = (value: string): string | undefined => 
        value.match(RegexPatterns.kebabSnakeCase)?.map(x => x.toLowerCase()).join('-')

    static toPascalCase = (value: string): string =>
        value.replace(RegexPatterns.pascalCase, x => x.charAt(0).toUpperCase() + x.substring(1).toLowerCase())

    static toSnakeCase = (value: string): string | undefined =>
        value.match(RegexPatterns.kebabSnakeCase)?.map(x => x.toLowerCase()).join('_')
}