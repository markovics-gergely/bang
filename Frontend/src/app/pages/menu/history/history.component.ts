import { Component, OnInit } from '@angular/core';
import { HistoryViewModel } from 'src/app/models';
import { HistoryService } from 'src/app/services/menu/history.service';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {
  public histories: HistoryViewModel[] | undefined;

  constructor(
    private historyService: HistoryService,
  ) { }

  ngOnInit(): void {
    this.historyService.getHistory().subscribe(
      response => {
        this.histories = response;
      }
    );
  }
}
