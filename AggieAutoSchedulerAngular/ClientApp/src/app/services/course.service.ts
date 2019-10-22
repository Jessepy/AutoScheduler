import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CRN } from '../classes/crn';
import { Course } from '../classes/course';
import { SubjectCoursePair } from '../classes/subject-course-pair';
import { isNullOrUndefined } from 'util';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private courses: Course[] = [];
  private subjectCoursePairs: SubjectCoursePair[] = [];

  constructor(private httpClient:HttpClient) { }

  validateCRN(crn: number): Observable<boolean> {
    return this.httpClient.get<boolean>("/api/Verification/CRN/crn=" + crn);
  }

  validateSubjectCourse(subject: string, courseNumber: number): Observable<boolean> {
    return this.httpClient.get<boolean>("/api/Verification/SubjectCourseNumber/subject=" + subject + "/courseNumber=" + courseNumber);
  }

  getSubjectCourseAutocomplete(subject: string, courseNumber: number): Observable<SubjectCoursePair[]> {
    return this.httpClient.get<SubjectCoursePair[]>("/api/Autocomplete/SubjectCourseNumber/subject=" + subject + "/courseNumber=" + courseNumber);
  }

  getCrnAutocomplete(toSearch: number): Observable<CRN[]> {
    return this.httpClient.get<number[]>("/api/Autocomplete/CRN/crn=" + toSearch).pipe(
      map(num => {
        let toReturn: CRN[] = [];
        if (isNullOrUndefined(num)) {
          return toReturn;
        }

        for (let i = 0; i < num.length; i++) {
          toReturn.push(new CRN(num[i]));
        }
        return toReturn;
      }));
  }

  addCourse(crn: CRN) {
    for (let i = 0; i < this.courses.length; i++) {
      if (this.courses[i].crn == crn.crn) {
        return;
      }
    }

    this.getCourse(crn).subscribe(returned => {
      if (returned) {
        console.log(returned);
        this.courses.push(returned);
      }
    })
  }

  addSubjectCourseNumber(scn: SubjectCoursePair): boolean {
    for (let i = 0; i < this.subjectCoursePairs.length; i++) {
      if (this.subjectCoursePairs[i].courseNumber === scn.courseNumber && this.subjectCoursePairs[i].subject === scn.subject) {
        return false;
      }
    }
    this.subjectCoursePairs.push(scn);
    return true;
  }

  getCourses(): Course[] {
    return this.courses;
  }

  getSubjectCoursePairs(): SubjectCoursePair[] {
    return this.subjectCoursePairs;
  }

  getCourse(crn: CRN): Observable<Course> {
    return this.httpClient.get<Course>("/api/Course/CRNSummary/crn=" + crn.crn);
  }


}
