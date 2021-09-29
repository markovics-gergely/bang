import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { OtherPlayer } from 'src/app/models';
import { CardService } from 'src/app/services/card.service';
import { CharacterService } from 'src/app/services/character.service';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-otherboard',
  templateUrl: './otherboard.component.html',
  styleUrls: ['./otherboard.component.css']
})
export class OtherboardComponent implements OnInit {
  @Input() player: OtherPlayer | undefined;
  constructor(private http: HttpClient, public roleService: RoleService, public characterService: CharacterService, public cardService: CardService) { }

  ngOnInit(): void {
  }

  counter(i: number) {
    return new Array(i);
  }
}
