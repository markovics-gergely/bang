import { Pipe, PipeTransform } from '@angular/core';
import { CardColorType } from '../models';

@Pipe({
  name: 'cardColorTypePipe'
})
export class CardColorTypePipe implements PipeTransform {
  private static lookup = ["Spades", "Clubs", "Hearts", "Diamonds"];
  transform(value: CardColorType): string {
    return CardColorTypePipe.lookup[value];
  }

}
