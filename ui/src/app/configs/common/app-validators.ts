import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms'

export class AppValidators {

    static isRequired(control: AbstractControl): ValidationErrors | null {
        return control && control.value && control.value.toString().trim().length > 0
            ? null
            : { noWhiteSpaces: true }
    }

    static isEnum(type: Record<string, unknown>): ValidatorFn {
        const error = { isEnum: true }
        return (control: AbstractControl): ValidationErrors | null => Object.values(type).includes(control.value)
                ? null
                : error
    }
}
