import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-gallery-control-panel',
  templateUrl: './gallery-control-panel.component.html',
  styleUrls: ['./gallery-control-panel.component.scss']
})
export class GalleryControlPanelComponent implements OnInit {

  @Output() flipCard: EventEmitter<boolean> = new EventEmitter();
  @Output() newBooth: EventEmitter<boolean> = new EventEmitter();

  @Output() galleryLoaded: EventEmitter<boolean> = new EventEmitter();

  @Input() refresh: Observable<boolean>;

  albums: any[] = [];
  constructor(private http: HttpService) { }

  ngOnInit() {
    this.galleryLoaded.emit(true);
    this.getSource();

    this.refresh.subscribe((result) => {
      if(result){
        this.getSource();
      }
    })
  }

  onEdit() {
    this.flipCard.emit(true);
  }

  play(album){
    this.http.post(`album/playalbum`, album).subscribe((result) => {
      this.albums.forEach(e => {
        e.playOnBooth = false
      });
      album.playOnBooth = true;
      this.newBooth.emit(true);
    });
  }

  getSource(){
    this.http.get(`album/getall`).subscribe((result) => {
      this.albums = result;
      this.albums.forEach(e => {
        e.photos.forEach(x => {
            x.image = "data:image/jpg;base64," + x.image;
        });
      });
      this.galleryLoaded.emit(false)
    });
  }
}
