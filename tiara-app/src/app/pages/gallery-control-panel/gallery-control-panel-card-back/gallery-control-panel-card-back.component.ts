import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NbDialogService } from '@nebular/theme';
import { HttpService } from 'src/environments/http.service';
import { ImageListComponent } from '../image-list/image-list.component';

@Component({
  selector: 'app-gallery-control-panel-card-back',
  templateUrl: './gallery-control-panel-card-back.component.html',
  styleUrls: ['./gallery-control-panel-card-back.component.scss']
})
export class GalleryControlPanelCardBackComponent implements OnInit {

  photosUploaded: any[] = [];
  photos2: any[] = []
  dataModel: any = {};
  formDataObject: any = {};
  @Output() refresh: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private dialogService: NbDialogService, private http: HttpService) { }

  ngOnInit(): void {
  }


  showImageList() {
    this.dialogService.open(ImageListComponent, {
      context: {
        title: 'Uploaded Images',
        photos: this.photosUploaded
      },
    }).onClose.subscribe((photos) => {
      this.photosUploaded = [];
      this.photosUploaded = photos;
      console.log(this.photosUploaded);
    });
  }

  onFileSelect(event){
    //push all files to an array
    for (var i = 0; i < event.target.files.length; i++) {
      this.photosUploaded.push(event.target.files[i]);
    }
    console.log(this.photosUploaded)
  }

  submitAlbum() {
    //bind images to formdata
    const formData = new FormData();
    for (var i = 0; i < this.photosUploaded.length; i++) {
      formData.append("file[]", this.photosUploaded[i]);
    }

    this.formDataObject = formData;

    this.http.post(`album/create/${this.dataModel.title}`, this.formDataObject).subscribe((result) => {
        this.refresh.emit(true)
        this.photosUploaded = [];
        this.dataModel.title = "";
    });

  }

}
