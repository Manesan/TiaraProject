import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-thoughts-tiara',
  templateUrl: './thoughts-tiara.component.html',
  styleUrls: ['./thoughts-tiara.component.scss']
})
export class ThoughtsTiaraComponent implements OnInit {
  girlThoughts: any[] =[];
  @Input() refresh: Observable<boolean>;
  constructor(private http: HttpService) { }

  ngOnInit(){

    this.getSource();


    this.refresh.subscribe((result) => {
      if(result){
        this.getSource();
      }
    })
  }


  getSource(){
    this.http.get(`thought/getgirlthoughts`).subscribe((result) => {
      console.log(result)
      this.girlThoughts = result;
    }, (error) => {
      console.log(error.error);
    });
  }

}
