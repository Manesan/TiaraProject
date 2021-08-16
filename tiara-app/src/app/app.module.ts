import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  NbThemeModule,
  NbLayoutModule,
  NbAccordionModule,
  NbActionsModule,
  NbAlertModule,
  NbButtonModule,
  NbCardModule,
  NbCheckboxModule,
  NbDatepickerModule,
  NbDialogModule,
  NbFormFieldModule,
  NbIconModule,
  NbInputModule,
  NbListModule,
  NbProgressBarModule,
  NbSelectModule,
  NbSpinnerModule,
  NbStepperModule,
  NbTabsetModule,
  NbToastrModule,
  NbTooltipModule,
  NbTreeGridModule,
  NbWindowModule,
  NbChatModule,
  NbToastrService,
  NbDialogService,
} from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { PhotoBoothComponent } from './pages/photo-booth/photo-booth.component';
import { NgbCarousel, NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { ThoughtsManesanComponent } from './pages/thoughts/thoughts-manesan/thoughts-manesan.component';
import { ThoughtsTiaraComponent } from './pages/thoughts/thoughts-tiara/thoughts-tiara.component';
import { PostsFeedComponent } from './pages/posts-feed/posts-feed.component';
import { PlannerComponent } from './pages/planner/planner.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { GalleryControlPanelComponent } from './pages/gallery-control-panel/gallery-control-panel.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ChatBoxComponent } from './pages/chat-box/chat-box.component';
import { ThoughtsCardBackComponent } from './pages/thoughts/thoughts-card-back/thoughts-card-back.component';
import { PlannerCardBackComponent } from './pages/planner/planner-card-back/planner-card-back.component';
import { GalleryControlPanelCardBackComponent } from './pages/gallery-control-panel/gallery-control-panel-card-back/gallery-control-panel-card-back.component';
import { PostsFeedCardBackComponent } from './pages/posts-feed/posts-feed-card-back/posts-feed-card-back.component';
import { HttpClientModule } from '@angular/common/http';
import { HttpService } from 'src/environments/http.service';
import { FormsModule } from '@angular/forms';
import { ImageListComponent } from './pages/gallery-control-panel/image-list/image-list.component';
import { LoginComponent } from './pages/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    PhotoBoothComponent,
    ThoughtsManesanComponent,
    ThoughtsTiaraComponent,
    PostsFeedComponent,
    PlannerComponent,
    GalleryControlPanelComponent,
    ChatBoxComponent,
    ThoughtsCardBackComponent,
    PlannerCardBackComponent,
    GalleryControlPanelCardBackComponent,
    PostsFeedCardBackComponent,
    ImageListComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'default' }),
    NbLayoutModule,
    NbEvaIconsModule,
    NbCardModule,
    NbIconModule,
    NbProgressBarModule,
    NbButtonModule,
    NbAlertModule,
    NbTabsetModule,
    NbSelectModule,
    NbInputModule,
    NbSpinnerModule,
    NbCheckboxModule,
    NbWindowModule,
    NbStepperModule,
    NbListModule,
    NbTreeGridModule,
    NbActionsModule,
    NbTooltipModule,
    NbAccordionModule,
    NbDatepickerModule,
    NbFormFieldModule,
    NgbCarouselModule,
    DragDropModule,
    FontAwesomeModule,
    NbChatModule,
    HttpClientModule,
    FormsModule,
    NbToastrModule.forRoot(),
    NbDialogModule.forChild()
  ],
  providers: [HttpClientModule, HttpService, NbToastrService, NbDialogService],
  bootstrap: [AppComponent],
})
export class AppModule {}
