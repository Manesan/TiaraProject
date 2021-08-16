import { Component, Input, OnInit } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';

@Component({
  selector: 'app-image-list',
  templateUrl: './image-list.component.html',
  styleUrls: ['./image-list.component.scss']
})
export class ImageListComponent implements OnInit {

  @Input() title: string;
  @Input() photos: any[];

  constructor(public dialogRef: NbDialogRef<any>) { }

  ngOnInit(): void {
  }

  close() {
    this.dialogRef.close(this.photos);
  }

  deletePhoto(photo){
    //remove photo
    this.photos.forEach( (item, index) => {
      if(item === photo) this.photos.splice(index,1);
    });
  }

}
