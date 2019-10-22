import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';

import { MatDialog } from '@angular/material/dialog';
import { MatSpinner } from '@angular/material/progress-spinner';

import { CourseService } from '../../services/course.service';

import { CRN } from '../../classes/crn';
import { SubjectCoursePair } from '../../classes/subject-course-pair';

const TIME_BEFORE_SEARCH = 300;
@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent implements OnInit {

  options$: Observable<object[]>;
  private searchTerms = new Subject<string>();

  inputValue: string;
  invalidInput: boolean = true;

  constructor(private courseService: CourseService, public loadingDialog: MatDialog) { }

  ngOnInit() {
    this.options$ = this.searchTerms
      .pipe(
        debounceTime(TIME_BEFORE_SEARCH),
        distinctUntilChanged(),
        switchMap((term: string) => {
          return this.getAutocomplete(term);
        }));
  }

  validateInput(input: string): void {
    if (!input) {
      this.invalidInput = true;
    } else {
      let partsOfInput: string[] = input.trim().split(" ");
      if (partsOfInput.length == 2) {
        let subject: string = partsOfInput[0];
        let courseNumber: number = Number(partsOfInput[1]);
        if (!isNaN(courseNumber)) {

          this.courseService.validateSubjectCourse(subject, courseNumber)
            .subscribe(validInput => {
              this.invalidInput = !validInput
            });
        }
      } else if (partsOfInput.length == 1) {
        let crn: number = Number(partsOfInput[0]);
        if (!isNaN(crn)) {
          this.courseService.validateCRN(crn)
            .subscribe(validInput => {
              this.invalidInput = !validInput
            });
        }
      } else {
        this.invalidInput = true;
      }
    }


  }

  search(input: string): void {
    this.searchTerms.next(input);
  }

  addCourse(input: string): void {
    input = input.trim();
    let loadingRef = this.loadingDialog.open(MatSpinner);
    loadingRef.disableClose = true;

    let possibleCrn: number = Number(input);
    if (!isNaN(possibleCrn)) {
      this.courseService.validateCRN(possibleCrn)
        .subscribe(isValid => {
          if (isValid) {
            this.courseService.addCourse(new CRN(possibleCrn));
          }

          loadingRef.close();
        });
    } else if (input.split(" ").length == 2) {
      let subject: string = input.split(" ")[0];
      let courseNumber: number = Number(input.split(" ")[1]);
      if (!isNaN(courseNumber)) {
        this.courseService.validateSubjectCourse(subject, courseNumber).subscribe(isValid => {
          if (isValid) {
            this.courseService.addSubjectCourseNumber(new SubjectCoursePair(subject, courseNumber));
          }

          loadingRef.close();
        });
      }
    } else {
      loadingRef.close();
    }
  }

  getAutocomplete(input: string): Observable<object[]> {

    if (!input || input.length < 3) {
      return of([]);
    }

    input = input.trim();
    let possibleCrn: number = Number(input);
    if (!isNaN(possibleCrn)) {
      return this.courseService.getCrnAutocomplete(possibleCrn);
    } else if (input.split(" ").length == 2) {
      let subject: string = input.split(" ")[0];
      let courseNumber: number = Number(input.split(" ")[1]);
      if (!isNaN(courseNumber)) {
        return this.courseService.getSubjectCourseAutocomplete(subject, courseNumber);
      }
    }
    return this.courseService.getSubjectCourseAutocomplete(input, -1);
  }
}
