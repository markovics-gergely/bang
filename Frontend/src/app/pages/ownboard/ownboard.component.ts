import { Component, Input, OnInit } from '@angular/core';
import { Player } from 'src/app/models';

@Component({
  selector: 'app-ownboard',
  templateUrl: './ownboard.component.html',
  styleUrls: ['./ownboard.component.css']
})
export class OwnboardComponent implements OnInit {
  @Input() player: Player | undefined;

  constructor() { }

  ngOnInit(): void {
    console.log(this.player);
  }

}
