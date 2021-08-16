import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-posts-feed',
  templateUrl: './posts-feed.component.html',
  styleUrls: ['./posts-feed.component.scss']
})
export class PostsFeedComponent implements OnInit {
  @Input() refresh: Observable<boolean>;

  posts: any[] = [];
  photos: any[] = [];
  @Output() loading: EventEmitter<boolean> = new EventEmitter<boolean>();


  constructor(private http: HttpService, private sanitizer: DomSanitizer) { }

  ngOnInit(){
    this.loading.emit(true);
    this.getSource();

    this.refresh.subscribe((result) => {
      if(result){
        this.getSource();
      }
    })
  }

  getSource(){
    this.http.get(`posts/getfeed`).subscribe((result) => {
      this.posts = result;
      console.log(this.posts)
      this.posts.forEach(e => {
        e.photo = "data:image/jpg;base64," + e.photo;
      });
      this.loading.emit(false);
    });
  }

}
