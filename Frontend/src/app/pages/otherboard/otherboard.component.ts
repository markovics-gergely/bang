import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { OtherPlayer } from 'src/app/models';

@Component({
  selector: 'app-otherboard',
  templateUrl: './otherboard.component.html',
  styleUrls: ['./otherboard.component.css']
})
export class OtherboardComponent implements OnInit {
  @Input() player: OtherPlayer | undefined;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    console.log(this.player);
  }

}
