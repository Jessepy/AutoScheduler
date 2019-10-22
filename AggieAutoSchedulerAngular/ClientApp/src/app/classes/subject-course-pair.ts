export class SubjectCoursePair {
  subject: string;
  courseNumber: number;
  crns: number[];
  constructor(subject?: string, courseNumber?: number, crns?: number[]) {
    this.subject = subject;
    this.courseNumber = courseNumber;
    this.crns = crns;
  }
}
