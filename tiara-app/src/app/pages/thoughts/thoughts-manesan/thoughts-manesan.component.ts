import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-thoughts-manesan',
  templateUrl: './thoughts-manesan.component.html',
  styleUrls: ['./thoughts-manesan.component.scss']
})
export class ThoughtsManesanComponent implements OnInit {
  boyThoughts: any[] =[];
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
    this.http.get(`thought/getboythoughts`).subscribe((result) => {
      console.log(result)
      this.boyThoughts = result;
    }, (error) => {
      console.log(error.error);
    });
  }

}
