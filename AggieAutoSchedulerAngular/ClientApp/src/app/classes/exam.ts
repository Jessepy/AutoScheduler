export class Exam {
  professor: string;
  location: string;
  startTime: Date;
  endTime: Date;
  constructor(professor?: string, location?: string, startTime?: Date, endTime?: Date) {
    this.professor = professor;
    this.location = location;
    this.startTime = startTime;
    this.endTime = endTime;
  }
}
