export class RegexPatterns {
    static kebabSnakeCase = /[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+/g
    static camelCase = /[^a-zA-Z0-9]+(.)/g
    static pascalCase = /\w\S*/g
    static spaceCamel = /([A-Z]+)/g
}
