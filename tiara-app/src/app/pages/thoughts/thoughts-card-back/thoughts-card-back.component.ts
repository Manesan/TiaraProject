import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-thoughts-card-back',
  templateUrl: './thoughts-card-back.component.html',
  styleUrls: ['./thoughts-card-back.component.scss']
})
export class ThoughtsCardBackComponent implements OnInit {
  dataModel: any = {};
  person: string;

  @Output() thoughtAdded: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpService) { }

  ngOnInit(){
    this.dataModel.active = true;
    this.dataModel.deleted = false;
    this.person = localStorage.getItem("person");
  }

  createThought(){
    this.http.post(`thought/createthought/${this.person}`, this.dataModel).subscribe((result) => {
      this.thoughtAdded.emit(true);
    }, (error) => {
      this.thoughtAdded.emit(false);
    });
  }

}
