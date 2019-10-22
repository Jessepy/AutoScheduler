import { Time } from "@angular/common";

export class Period {
  dayOfWeek: string;
  location: string;
  professor: string;
  startTime: Time;
  endTime: Time;

  constructor(dayOfWeek?: string, location?: string, professor?: string,
    startTime?: Time, endTime?: Time
  ) {
    this.dayOfWeek = dayOfWeek;
    this.location = location;
    this.professor = professor;
    this.startTime = startTime;
    this.endTime = endTime;
  }
}
