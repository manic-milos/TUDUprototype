import { Component, OnInit, Inject, Input } from '@angular/core';
import {HttpClient} from '@angular/common/http';

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
    this.http.get<ITaskItem[]>(this.baseUrl + 'api/TaskItems/TaskItems')
    .subscribe(result => {
      console.log(result);
        this.tasks = result;
        console.log(this.tasks);
      }, error => console.error(error));
  }

  addNewTask() {
    // console.log(this.ListID);
    var newtask=this.newTask;
    this.http.post<ITaskItem>(
      this.baseUrl + 'api/TaskItems/TaskItem', newtask)
    .subscribe(result => {
      console.log(result);
      this.loadTasks();
      this.newTask = new TaskItem();
      }, error => console.log(error));

  }


}

interface ITaskItem {
  id: number;
  taskName: string;
  originalProjectID: number;
}


export class TaskItem implements ITaskItem
{
  id: number;
  taskName: string;
  originalProjectID: number;

  constructor() {
    this.id = null;
    this.taskName = '';
    this.originalProjectID = 5;
  }
}
