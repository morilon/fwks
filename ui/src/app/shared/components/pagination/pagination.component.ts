import { Component, EventEmitter, forwardRef, Input, Output } from '@angular/core'
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms'

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  providers: [
    { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => PaginationComponent), multi: true }
  ]
})
export class PaginationComponent implements ControlValueAccessor {

  @Input() collection: any[]
  @Input() pageSize: number
  @Input() collectionSize: number

  @Output() pageChange = new EventEmitter<number>()

  private currentPage: number

  public get page(): number {
    return this.currentPage
  }

  public set page(newPage: number) {
    if (newPage !== this.currentPage) {
      this.currentPage = newPage
      this.onChange(newPage)
    }
  }

  constructor() {
    this.currentPage = 1
    this.collection = []
    this.pageSize = 10
    this.collectionSize = 0
  }

  onChange(_: any): void { }

  onTouch(): void { }

  writeValue(newPage: number): void {
    this.currentPage = newPage
  }

  registerOnChange(fn: any): void {
    this.onChange = fn
  }

  registerOnTouched(fn: any): void {
    this.onTouch = fn
  }

  setDisabledState?(): void { }

  pageChanged(newPage: number): void {
    this.page = newPage
    this.pageChange.emit(this.page)
  }
}
