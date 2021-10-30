import { Injectable } from '@angular/core';
import { Card, CharacterType, HoverEnum, RoleType } from 'src/app/models';

@Injectable({
  providedIn: 'root'
})
export class HoverService {
  
  hovered: boolean = false;
  hoveredCard: Card | undefined;
  hoveredRole: RoleType | undefined;
  hoveredCharacter: CharacterType | undefined;
  
  constructor() { }

  public setCardHovered(hoverData: {data: string, type: HoverEnum}) {
    if(!hoverData) {
      this.hovered = false;
      this.hoveredCard = undefined;
      this.hoveredCharacter = undefined;
      this.hoveredRole = undefined;
    } else if(hoverData.type === HoverEnum.Card) {
      if(hoverData.data) {
        this.hovered = true;
        this.hoveredCard = JSON.parse(hoverData.data);
      } else {
        this.hovered = false;
        this.hoveredCard = undefined;
      }
    } else if(hoverData.type === HoverEnum.Character) {
      if(hoverData.data) {
        this.hovered = true;
        this.hoveredCharacter = JSON.parse(hoverData.data);
      } else {
        this.hovered = false;
        this.hoveredCard = undefined;
      }
    } else if(hoverData.type === HoverEnum.Role) {
      if(hoverData.data) {
        this.hovered = true;
        this.hoveredRole = JSON.parse(hoverData.data);
      } else {
        this.hovered = false;
        this.hoveredCard = undefined;
      }
    }
  }

  public setOnlyCardHovered(hoverData: string) {
    if(hoverData) {
      this.hovered = true;
      this.hoveredCard = JSON.parse(hoverData);
    } else {
      this.hovered = false;
      this.hoveredCard = undefined;
    }
  }
}
