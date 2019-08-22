import { Component, OnInit, Inject } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  public tasks: TaskItem[];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string ) {
    http.get<TaskItem[]>(baseUrl + 'api/TaskItems/TaskItems')
    .subscribe(result => {
      console.log(result);
        this.tasks = result;
        console.log(this.tasks);
      }, error => console.error(error));
  }
  ngOnInit() {
  }

}

interface TaskItem {
  id: number;
  taskName: string;
  originalProjectID: number;
}
