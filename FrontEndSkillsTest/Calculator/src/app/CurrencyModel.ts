export interface CurrencyModel {
  table: string
  currency: string
  code: string
  rates: Rate[]
}

export interface Rate {
  no: string
  effectiveDate: string
  mid: number
}
