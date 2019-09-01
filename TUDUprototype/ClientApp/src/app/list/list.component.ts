import { Component, OnInit, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITaskList } from '../lists/lists.component';

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
  newTask: ITaskInList;

  constructor(http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  getTasks() {
    this.http.get<ITaskInList[]>(
      this.baseUrl + 'api/TaskItems/' + this.List.listID)
      .subscribe(result => {
        this.tasks = result;
        console.log(this.tasks);
        console.log(this);
      },
        error => console.error(error));
  }

  ngOnInit() {
    this.getTasks();
    this.newTask = {
      taskID: null,
      taskName: '',
      orderNo: null
    };
  }

  sort(): void {
    this.tasks.sort((a, b) => {
      return a.orderNo - b.orderNo;
    });
  }

  public up(task: ITaskInList) {
    if (task.orderNo == 1) {

      console.log("task (" + task.taskID + "," + task.taskName + ") is at the highest position");
      return;
    }
    this.tasks[task.orderNo - 2].orderNo++;
    this.tasks[task.orderNo - 1].orderNo--;

    this.sort();

    console.log(task);
  }
  public down(task: ITaskInList) {
    if (task.orderNo == this.tasks.length) {
      console.log("task (" + task.taskID + "," + task.taskName + ") is at the lowest position");
      return;
    }
    this.tasks[task.orderNo].orderNo--;
    this.tasks[task.orderNo - 1].orderNo++;

    this.sort();

    console.log(task);
  }

  public update() {
    this.http.put(this.baseUrl + 'api/TaskInList/ReplaceTasks/' + this.List.listID, this.tasks)
      .subscribe((x) => {
        console.log("successfully updated");
        this.getTasks();
      }, (error) => console.log(error));
  }


  addNewTask() {
    this.newTask.orderNo = this.tasks.length + 1;
    this.tasks.push(this.newTask);
    this.newTask = {
      taskID: null,
      taskName: '',
      orderNo: null
    };
  }

}

interface ITaskInList {
  taskID: number;
  taskName: string;
  orderNo: number;
}
