import { Component, OnInit, Inject,Input } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ITaskList} from '../lists/lists.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  @Input() List: ITaskList;
  http: HttpClient;
  baseUrl: string;
  tasks: ITaskInList[];

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
      this.http = http;
      this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.http.get<ITaskInList[]>(
      this.baseUrl + 'api/TaskItems/' + this.List.listID)
      .subscribe(result => {
        this.tasks = result;
        console.log(this.tasks);
        console.log(this);
      },
        error => console.error(error));
  }

}

interface ITaskInList {
  taskID: number;
  taskName: string;
  orderNo: number;
}
