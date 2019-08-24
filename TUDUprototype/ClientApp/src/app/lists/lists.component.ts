import { Component, OnInit , Inject} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  baseUrl: string;
  http: HttpClient;
  lists: ITaskList[];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }



  ngOnInit() {
    this.http.get<ITaskList[]>(this.baseUrl + 'api/TaskLists/TaskLists')
    .subscribe(result => {
      this.lists = result;
    }, error => console.log(error));
  }

}

interface ITaskList {
  listID: number;
  listName: string;
  projectID: number;
}
