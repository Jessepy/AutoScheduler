import { Period } from './period';
import { Exam } from './exam';
import { CdkRowDef } from '@angular/cdk/table';

export class Course {
  crn: number;
  subject: string;
  courseNumber: number;
  sectionNumber: number;
  credits: number;
  title: string;
  monday: Period[] = [];
  tuesday: Period[] = [];
  wednesday: Period[] = [];
  thursday: Period[] = [];
  friday: Period[] = [];
  exams: Exam[] = [];

  constructor(
    crn: number, subject: string, courseNumber: number,
    sectionNumber: number, credits: number, title: string,
    monday: Period[] = [], tuesday: Period[] = [], wednesday: Period[] = [],
    thursday: Period[] = [], friday: Period[] = [], exams: Exam[] = []
  ) {
    this.crn = crn;
    this.subject = subject;
    this.courseNumber = courseNumber;
    this.sectionNumber = sectionNumber;
    this.credits = credits;
    this.title = title;
    this.monday = monday;
    this.tuesday = tuesday;
    this.wednesday = wednesday;
    this.thursday = thursday;
    this.friday = friday;
    this.exams = exams;
  }


}
