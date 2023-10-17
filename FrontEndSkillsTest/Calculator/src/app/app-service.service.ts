import { Injectable } from '@angular/core';
import { Subject, tap } from 'rxjs';
import { AppEndpointsService } from './app-endpoints.service';
import { CurrencyModel } from './CurrencyModel';

@Injectable({
  providedIn: 'root'
})
export class AppServiceService {

  firstOperation: boolean = true;
  memoryBankNumber: number = 0;
  memoryBankOperation: string = '';

  getResult$ = new Subject<string>();
  error$ = new Subject<boolean>();
  firstComa$ = new Subject<boolean>();
  firstOperation$ = new Subject<boolean>();
  constructor(private appEndPoints: AppEndpointsService) {
  }

  onOperation(operation: string, numberToOperate: number) {
    if (this.firstOperation && operation != '=') {
      this.firstOperation = false;
      this.memoryBankNumber = numberToOperate;
      this.memoryBankOperation = operation;
      this.firstOperation$.next(true);
      this.firstComa$.next(true);
    }
    else {
      switch (operation) {
        case '=': {
          if (this.firstOperation) {
            break;
          }
          else {
            let result = this.mathOperation(this.memoryBankOperation, this.memoryBankNumber, numberToOperate);
            this.getResult$.next(String(result));
            this.firstOperation = true;
            this.firstOperation$.next(this.firstOperation);
            this.firstComa$.next(true);
            this.memoryBankNumber = 0;
            this.memoryBankOperation = '';
          }
          break;
        }
        default: {
          this.memoryBankNumber = this.mathOperation(this.memoryBankOperation, this.memoryBankNumber, numberToOperate);
          this.getResult$.next(String(this.memoryBankNumber));
          this.firstComa$.next(true);
          this.firstOperation$.next(true);
          this.memoryBankOperation = operation;
          break;
        }
      }
    }
  }

  mathOperation(operation: string, leftSide: number, rightSide: number): number {
    let result = 0;
    switch (operation) {
      case '/': {
        if (rightSide === 0) {
          console.log(rightSide);
          this.getResult$.next('ERROR: Divide By 0');
          this.error$.next(true);
          break;
        }
        else {
          result = leftSide / rightSide;
          break;
        }
      }
      case '+': {
        result = leftSide + rightSide;
        break;
      }
      case '-': {
        result = leftSide - rightSide;
        break;
      }
      case '*': {
        result = leftSide * rightSide;
        break;
      }
      default: break;
    }
    return result;
  }

  calculateCurrency(currency: string, numberToOperate: number) {
    let currencyValue: CurrencyModel;
    this.appEndPoints.getCurrency(currency).pipe(
      tap((data: CurrencyModel) => currencyValue = data),
      tap(_ => this.getResult$.next(String((numberToOperate / currencyValue!.rates.pop()!.mid).toFixed(2))))
    )
      .subscribe();

    this.firstComa$.next(true);
    this.firstOperation$.next(true);
    
  }

  onClean() {
    this.error$.next(false);
    this.firstOperation = true;
    this.memoryBankNumber = 0;
    this.memoryBankOperation = '';
    this.getResult$.next('0');
    this.firstOperation$.next(true);
    this.firstComa$.next(true);
  }

}
