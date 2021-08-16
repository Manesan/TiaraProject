import { Component, Input, OnInit } from '@angular/core';
import {CdkDragDrop, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import { HttpService } from 'src/environments/http.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-planner',
  templateUrl: './planner.component.html',
  styleUrls: ['./planner.component.scss']
})
export class PlannerComponent implements OnInit {

  todo: any[] =[];
  done: any[] = [];

  containers: any = {};

  @Input() refresh: Observable<boolean>;


  constructor(private http: HttpService) { this.getContainers();}

  ngOnInit(){
    //this.getContainers();
    this.refresh.subscribe((result) =>{
      this.getContainers();
    });

  }

  getContainers(){
    this.todo = [];
    this.done = [];

    this.http.get(`milestone/getall`).subscribe((result) => {
      result.forEach(e => {
        if(e.achieved == true){
          this.done.push(e);
        }
        else{
          this.todo.push(e);
        }
      });

    });
  }

  async drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
     this.updateStatus();
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
                        await this.updateStatus();
    }

  }

  async updateStatus(){
    this.todo.forEach(e => {
      e.achieved = false;
    });

    this.done.forEach(e => {
      e.achieved = true;
    });

    await this.updateContainers();

    this.http.post(`milestone/updatemilestonecontainers`, this.containers).subscribe();

  }


  async updateContainers(){
    this.containers.todo = this.todo;
    this.containers.done = this.done;
  }

}
