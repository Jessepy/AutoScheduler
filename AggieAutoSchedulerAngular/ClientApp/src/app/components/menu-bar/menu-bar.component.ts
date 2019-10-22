import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { EditCoursesDialogComponent } from '../edit-courses-dialog/edit-courses-dialog.component';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.css']
})
export class MenuBarComponent implements OnInit {

  constructor(public dialogBox: MatDialog) { }

  ngOnInit() {
  }

  openClassPrefsWindow() {
    let dialogRef = this.dialogBox.open(EditCoursesDialogComponent, {
      height: '70%',
      width: '60%',
    });
  }

}
