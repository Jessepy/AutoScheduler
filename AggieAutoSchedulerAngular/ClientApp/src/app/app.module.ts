import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './classes/material.module';
import { MatSpinner } from '@angular/material/progress-spinner';

import { AppComponent } from './app.component';
import { AddCourseComponent } from './components/add-course/add-course.component';
import { EditCoursesDialogComponent } from './components/edit-courses-dialog/edit-courses-dialog.component';
import { MenuBarComponent } from './components/menu-bar/menu-bar.component';
import { CourseListComponent } from './components/course-list/course-list.component';
import { PeriodComponent } from '../period/period.component';

@NgModule({
  entryComponents: [
    EditCoursesDialogComponent,
    MatSpinner
  ],
  declarations: [
    AppComponent,
    AddCourseComponent,
    EditCoursesDialogComponent,
    MenuBarComponent,
    CourseListComponent,
    PeriodComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
