using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace AAS.ScheduleMaker
{
    public class ScheduleMaker
    {
        public static List<Schedule> MakeSchedules(DB.Data.AggieAutoSchedulerContext _context, List<SubjectCoursePair> subjectCoursePairs, List<int> crns, List<SubjectCourseSection> subjectCourseSection)
        {
            List<List<Course>> courses = GetCourses(_context, subjectCoursePairs, crns, subjectCourseSection);

            List<Schedule> toReturn = new List<Schedule>();
            Recursive(toReturn, courses, new Schedule(), 0);            

            return toReturn;
        }

        private static void Recursive(List<Schedule> toReturn, List<List<Course>> courses, Schedule dummySchedule,int index)
        {
            foreach(Course course in courses[index])
            {
                if(index == 0)
                {
                    dummySchedule.Courses.Clear();
                }

                if(dummySchedule.FitsInSchedule(course))
                {
                    dummySchedule.Courses.Add(course);
                    if(index == courses.Count - 1)
                    {
                        Schedule toAdd = new Schedule();
                        toAdd.Courses.AddRange(dummySchedule.Courses);
                        toReturn.Add(toAdd);
                    }
                    else
                    {
                        Recursive(toReturn, courses, dummySchedule, index + 1);
                    }

                    dummySchedule.Courses.RemoveAt(dummySchedule.Courses.Count - 1);
                }

            }

        }

        private static List<List<Course>> GetCourses(DB.Data.AggieAutoSchedulerContext _context, List<SubjectCoursePair> subjectCoursePairs, List<int> crns, List<SubjectCourseSection> subjectCourseSections)
        {
            subjectCoursePairs = subjectCoursePairs.Distinct().ToList();
            crns = crns.Distinct().ToList();
            subjectCourseSections = subjectCourseSections.Distinct().ToList();

            List<List<Course>> courses = new List<List<Course>>();

            // Add crns
            foreach (int crn in crns)
            {
                List<DB.Course> l = _context.Course.
                    Include(course => course.Periods).
                    Include(course => course.Exams).
                    Where(course => course.CRN == crn)
                    .ToList();

                if (l.Count > 0)
                {
                    List<Course> toAdd = new List<Course>();
                    toAdd.Add(new Course(l[0]));
                    courses.Add(toAdd);
                }
            }

            // add subject/coursenumber pairs
            foreach (SubjectCoursePair scp in subjectCoursePairs)
            {
                List<DB.Course> l = _context.Course.
                    Include(course => course.Periods).
                    Include(course => course.Exams).
                    Where(course => new SubjectCoursePair(course.Subject, course.CourseNumber) == scp)
                    .ToList();

                if (l.Count > 0)
                {
                    List<Course> toAdd = new List<Course>();
                    foreach(DB.Course c in l)
                    {
                        toAdd.Add(new Course(c));
                    }
                    courses.Add(toAdd);
                }
            }

            // add subject/coursenumber/sectionnumbers
            foreach (SubjectCourseSection scs in subjectCourseSections)
            {
                List < DB.Course > l = _context.Course.
                    Include(course => course.Periods).
                    Include(course => course.Exams).
                    Where(course => new SubjectCourseSection(course.Subject, course.CourseNumber, course.SectionNumber) == scs)
                    .ToList();

                if(l.Count > 0)
                {
                    List<Course> toAdd = new List<Course>();
                    toAdd.Add(new Course(l[0]));
                    courses.Add(toAdd);
                }

            }

            return courses;
        }
    }
}
