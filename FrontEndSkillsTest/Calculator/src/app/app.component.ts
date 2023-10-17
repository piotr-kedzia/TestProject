import { Component } from '@angular/core';
import { AppEndpointsService } from './app-endpoints.service';
import { AppServiceService } from './app-service.service';
import { Subject, takeUntil, tap } from 'rxjs';
import { parse } from 'jest-editor-support';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Calculator';
  result: string = '0';
  destroyed$ = new Subject();
  error: boolean = false;
  firstComa: boolean = true;
  firstOperation: boolean = true;
  memoryMinus: boolean = false;
  constructor(private appService: AppServiceService, private appEndpoints: AppEndpointsService) {

    this.appService.getResult$.pipe(
      tap(data => {
        if(!this.error)
          this.result = data
      }),
      takeUntil(this.destroyed$)
    )
      .subscribe();

    this.appService.error$.pipe(
      tap(data => this.error = data),
      takeUntil(this.destroyed$)
    )
      .subscribe();

    this.appService.firstComa$.pipe(
      tap(data => this.firstComa = data),
      takeUntil(this.destroyed$)
    )
      .subscribe();

    this.appService.firstOperation$.pipe(
      tap(data => this.firstOperation = data),
      takeUntil(this.destroyed$)
    )
      .subscribe();

  }

  onNumberOrComaClick(numberOrComa: string) {
    if (this.error) 
      return;
    
    if (this.result.length > 14)
      return;

    if (!this.firstComa && numberOrComa === '.')
      return;
    else if (numberOrComa === '.')
      this.firstComa = false;

      

    if (this.firstOperation && numberOrComa != '.') {
      this.result = numberOrComa;
      this.firstOperation = false;
    }
    else if (this.firstOperation && numberOrComa === '.') {
      this.result += numberOrComa;
      this.firstOperation = false;
    }
    else
      this.result += numberOrComa;

    if (this.memoryMinus && numberOrComa != '.') {
      this.result = '-' + this.result
      this.memoryMinus = false;
    }
    
    
  }

  onMathOperationClick(operation: string) {
    if (this.error)
      return;

    if (operation === '-' && this.firstOperation) {
      this.memoryMinus = true;
      return;
    }

    this.memoryMinus = false;
    let numberToOperate = parseFloat(this.result);
    console.log(operation + numberToOperate);
    this.appService.onOperation(operation, numberToOperate);
  }

  onCurrencyClick(currency: string) {
    if (this.error)
      return;

    let numberToOperate = parseFloat(this.result);
    if (numberToOperate <= 0)
      return;

    this.appService.calculateCurrency(currency, numberToOperate);

  }

  onCleanClick() {
    this.memoryMinus = false;
    this.appService.onClean();
  }

  ngOnDestroy(): void {
    this.destroyed$.complete();
  }
}
