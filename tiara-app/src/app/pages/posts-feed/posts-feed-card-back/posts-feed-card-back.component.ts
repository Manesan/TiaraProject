import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-posts-feed-card-back',
  templateUrl: './posts-feed-card-back.component.html',
  styleUrls: ['./posts-feed-card-back.component.scss']
})
export class PostsFeedCardBackComponent implements OnInit {

  dataModel: any = {};
  formDataObject: any;
  @Output() refresh: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private http: HttpService) { }

  ngOnInit(){
  }

  onFileSelect(event){
    var files = event.target.files;
    var file = files[0];

    console.log(file)

    const formData = new FormData();
    formData.append('file', file, file.name);

    this.formDataObject = formData;
  }

  submit() {
    this.http.post(`posts/createpost/${this.dataModel.caption}/${this.dataModel.location}/${localStorage.getItem('person')}`, this.formDataObject).subscribe((result) => {
      this.refresh.emit(true)
    });
  }
}
