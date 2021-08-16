import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { NgbCarousel, NgbSlideEvent, NgbSlideEventSource } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-photo-booth',
  templateUrl: './photo-booth.component.html',
  styleUrls: ['./photo-booth.component.scss']
})
export class PhotoBoothComponent implements OnInit {

  constructor(private http: HttpService, private sanitizer: DomSanitizer) { }

  album: any = {};
  images: any[] = [];

  @Input() refresh: Observable<boolean>;
  @Output() boothLoaded: EventEmitter<boolean> = new EventEmitter();

  ngOnInit() {
    this.getSource();

    this.refresh.subscribe((result) => {
      if(result){
        this.getSource();
      }
    })
  }

  getSource() {
    this.images = [];
    this.http.get(`album/getboothalbum`).subscribe((result) => {
      this.album = result;
      // console.log(this.album)
      this.album.photos.forEach(e => {
        e.image = "data:image/jpg;base64," + e.image;
        this.images.push(e.image);
        // console.log(this.images)
        this.boothLoaded.emit(true);
      });
    });
  }
  // images = [62, 83, 466, 965, 982, 1043, 738].map((n) => `https://picsum.photos/id/${n}/900/500`);

  paused = false;
  unpauseOnArrow = false;
  pauseOnIndicator = false;
  pauseOnHover = true;
  pauseOnFocus = true;

  @ViewChild('carousel', {static : true}) carousel: NgbCarousel;

  togglePaused() {
    if (this.paused) {
      this.carousel.cycle();
    } else {
      this.carousel.pause();
    }
    this.paused = !this.paused;
  }

  onSlide(slideEvent: NgbSlideEvent) {
    if (this.unpauseOnArrow && slideEvent.paused &&
      (slideEvent.source === NgbSlideEventSource.ARROW_LEFT || slideEvent.source === NgbSlideEventSource.ARROW_RIGHT)) {
      this.togglePaused();
    }
    if (this.pauseOnIndicator && !slideEvent.paused && slideEvent.source === NgbSlideEventSource.INDICATOR) {
      this.togglePaused();
    }
  }
}
