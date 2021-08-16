import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NbToastrService } from '@nebular/theme';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-planner-card-back',
  templateUrl: './planner-card-back.component.html',
  styleUrls: ['./planner-card-back.component.scss']
})
export class PlannerCardBackComponent implements OnInit {

  description: string = "";
  @Output() flipCard: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpService, private toaster: NbToastrService) { }

  ngOnInit() {
  }


  createMilestone(){
    if(this.description != ""){
      this.http.get(`milestone/createmilestone/${this.description}`).subscribe((result)=> {
        this.flipCard.emit(true);
      });
    }
    else{
      this.toaster.warning("Please provide a description", "Oops");
    }
  }

}
