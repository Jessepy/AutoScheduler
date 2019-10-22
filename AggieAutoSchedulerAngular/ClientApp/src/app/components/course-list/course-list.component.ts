import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { SubjectCoursePair } from '../../classes/subject-course-pair';
import { Course } from '../../classes/course';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {

  courses: Course[];
  subjectCoursePairs: SubjectCoursePair[];

  constructor(private courseService: CourseService) { }

  ngOnInit() {
    this.courses = this.courseService.getCourses();
    this.subjectCoursePairs = this.courseService.getSubjectCoursePairs();
  }

}
