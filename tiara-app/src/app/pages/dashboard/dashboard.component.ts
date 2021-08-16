import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  boyLoggedIn: boolean = false;
  girlLoggedIn: boolean = false;

  flippedControlPanel: boolean = false;
  flippedTiaraThoughts: boolean = false;
  flippedManesanThoughts: boolean = false;
  flippedPlanner: boolean = false;
  flippedPosts: boolean = false;


  refresh: Subject<boolean> = new Subject<boolean>();
  refreshPost: Subject<boolean> = new Subject<boolean>();
  refreshGalleryPanel: Subject<boolean> = new Subject<boolean>();
  changeBooth: Subject<boolean> = new Subject<boolean>();
  refreshThoughts: Subject<boolean> = new Subject<boolean>();

  loadingPosts: boolean = true;
  loadingBooth: boolean = true;
  loadingGallery: boolean = true;

  constructor(private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(){
    if(!("api" in localStorage && "person" in localStorage)){
      this.router.navigate([''], { relativeTo: this.route });
    }


    this.boyLoggedIn = false;
    this.girlLoggedIn = false;

    let person = localStorage.getItem("person");
    console.log(person)

    if(person == "Boy"){
      this.boyLoggedIn = true;
      this.girlLoggedIn = false;
    }
    else{
      this.girlLoggedIn = true;
      this.boyLoggedIn = false;
    }

    console.log(this.girlLoggedIn)
  }

  toggleControlPanelView(){
    this.flippedControlPanel = !this.flippedControlPanel;
  }

  thoughtAdded($event) {
    if($event){
      let person = localStorage.getItem("person");
      if(person == "Boy"){
        this.toggleManesanThoughtsView();
      }
      else{
        this.toggleTiaraThoughtsView();
      }
      this.refreshThoughts.next(true);
    }
  }

  toggleTiaraThoughtsView(){
    this.flippedTiaraThoughts = !this.flippedTiaraThoughts;
  }

  toggleManesanThoughtsView(){
    this.flippedManesanThoughts = !this.flippedManesanThoughts;
  }

  togglePlannerView(){
    this.flippedPlanner = !this.flippedPlanner;
  }

  togglePostsView(){
    this.flippedPosts = !this.flippedPosts;
  }

  mileStoneAdded() {
    this.togglePlannerView();
    this.refresh.next();

  }

  refreshPosts($event){
    console.log($event)
    this.togglePostsView();
    this.refreshPost.next(true);
  }

  refreshGallery($event) {
    this.toggleControlPanelView();
    this.refreshGalleryPanel.next(true);
  }

  newBooth($event) {
    if($event){
      this.loadingBooth = true;
      this.changeBooth.next(true);
    }
  }

  boothLoaded($event){
    if($event){
      this.loadingBooth = false;
    }
  }

  logout() {
    localStorage.removeItem('api')
    localStorage.removeItem('person')
    this.router.navigate([''], { relativeTo: this.route });
  }

}
