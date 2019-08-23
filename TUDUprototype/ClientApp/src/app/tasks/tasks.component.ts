import { Component, OnInit, Inject } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { httpFactory } from '@angular/platform-server/src/http';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  public tasks: TaskItem[];
  public newTaskName: string;
  m_http: HttpClient;
  m_baseUrl: string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string ) {
    this.m_http = http;
    this.m_baseUrl = baseUrl;
    this.loadTasks();
  }
  ngOnInit() {
  }

  loadTasks() {
    this.m_http.get<TaskItem[]>(this.m_baseUrl + 'api/TaskItems/TaskItems')
    .subscribe(result => {
      console.log(result);
        this.tasks = result;
        console.log(this.tasks);
      }, error => console.error(error));
  }

  addNewTask(input_element: any) {
    var newtask = {
      id: null,
      taskName: input_element.value,
      originalProjectID: 5
    };
    this.m_http.post<TaskItem>(
      this.m_baseUrl + 'api/TaskItems/TaskItem', newtask)
    .subscribe(result => {
      console.log(result);
      this.loadTasks();
      input_element.value = '';
      }, error => console.log(error));

  }


}

interface TaskItem {
  id: number;
  taskName: string;
  originalProjectID: number;
}
