import { Pipe, PipeTransform } from '@angular/core';
import { Card } from '../models';

@Pipe({
  name: 'cast'
})
export class CastPipe implements PipeTransform {

  transform(value: any, args?: any): Card {
    return value;
  }

}
