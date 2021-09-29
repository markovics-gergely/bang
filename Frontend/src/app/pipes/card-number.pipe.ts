import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cardNumber'
})
export class CardNumberPipe implements PipeTransform {
  private static lookup = ["Ace", "1", "2", "3", 
                           "4", "5", "6", "7", 
                           "8", "9", "10", "Jack", 
                           "Queen", "King"];
  transform(value: number): string {
    return CardNumberPipe.lookup[value];
  }

}
