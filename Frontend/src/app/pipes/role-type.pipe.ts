import { Pipe, PipeTransform } from '@angular/core';
import { RoleType } from '../models';

@Pipe({
  name: 'roleType'
})
export class RoleTypePipe implements PipeTransform {
  private static lookup = ["Outlaw", "Renegade", "Sheriff", "Vice"];
  transform(value: RoleType): string {
    return RoleTypePipe.lookup[value];
  }

}
