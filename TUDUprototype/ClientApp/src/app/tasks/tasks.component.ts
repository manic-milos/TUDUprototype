import { Component, OnInit, Inject, Input } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { unescapeIdentifier } from '@angular/compiler';
import { isUndefined } from 'util';


interface ITaskItem {
  id: number;
  taskName: string;
  originalProjectID: number;
}


export class TaskItem implements ITaskItem {
  id: number;
  taskName: string;
  originalProjectID: number;


  constructor() {
    this.id = null;
    this.taskName = '';
    this.originalProjectID = 5;
  }
}

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  @Input() ListID: number;
  public tasks: ITaskItem[];
  public newTask: ITaskItem;
  http: HttpClient;
  baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string ) {
    this.http = http;
    this.baseUrl = baseUrl;
  }
  ngOnInit() {
    this.loadTasks();
    this.newTask = new TaskItem();
  }

  loadTasks() {
    console.log(this.ListID);
    let query = '';
    if (!isUndefined(this.ListID)) {
      query += '?ListID=' + this.ListID;
    }
    this.http.get<ITaskItem[]>(this.baseUrl + 'api/TaskItems/TaskItems' + query)
    .subscribe(result => {
      console.log(result);
        this.tasks = result;
        console.log(this.tasks);
      }, error => console.error(error));
  }

  addNewTask() {
    let query = '';
    if (!isUndefined(this.ListID)) {
      query += '?ListID=' + this.ListID;
    }
    this.http.post<ITaskItem>(
      this.baseUrl + 'api/TaskItems/TaskItem' + query, this.newTask)
    .subscribe(result => {
      console.log(result);
      this.loadTasks();
      this.newTask = new TaskItem();
      }, error => console.log(error));

  }


}

