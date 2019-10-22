using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace AAS.ScheduleMaker
{
    public class Course
    {
        public Course()
        {
            Monday = new List<Period>();
            Tuesday = new List<Period>();
            Wednesday = new List<Period>();
            Thursday = new List<Period>();
            Friday = new List<Period>();
            Exams = new List<Exam>();
        }

        public Course(AAS.DB.Course _course) : this()
        {
            CopyData(_course);
        }

        public Course(int _crn) : this()
        {
            AAS.DB.Data.AggieAutoSchedulerContext context = new DB.Data.AggieAutoSchedulerContext(new Microsoft.EntityFrameworkCore.DbContextOptions<DB.Data.AggieAutoSchedulerContext>());
            List<AAS.DB.Course> courses = context.Course.
                Include(course => course.Periods).
                Include(course => course.Exams).
                Where(course => course.CRN == _crn)
                .ToList();

            if (courses.Count > 0)
            {
                CopyData(courses[0]);
            }                
        }

        public int CRN { get; set; }
        public string Subject { get; set; }
        public int Credits { get; set; }
        public string Title { get; set; }
        public int CourseNumber { get; set; }
        public int SectionNumber { get; set; }
        public List<Period> Monday { get; set; }
        public List<Period> Tuesday { get; set; }
        public List<Period> Wednesday { get; set; }
        public List<Period> Thursday { get; set; }
        public List<Period> Friday { get; set; }
        public List<Exam> Exams { get; set; }

        public void CopyData(AAS.DB.Course other)
        {
            CRN = other.CRN;
            Subject = other.Subject;
            Credits = other.Credits;
            Title = other.Title;
            CourseNumber = other.CourseNumber;
            SectionNumber = other.SectionNumber;

            foreach (AAS.DB.Period p in other.Periods)
            {
                if (p.DayOfWeek.ToUpper() == "M") { Monday.Add(new Period(p)); }
                if (p.DayOfWeek.ToUpper() == "T") { Tuesday.Add(new Period(p)); }
                if (p.DayOfWeek.ToUpper() == "W") { Wednesday.Add(new Period(p)); }
                if (p.DayOfWeek.ToUpper() == "R") { Thursday.Add(new Period(p)); }
                if (p.DayOfWeek.ToUpper() == "F") { Friday.Add(new Period(p)); }
            }

            foreach (AAS.DB.Exam e in other.Exams)
            {
                Exams.Add(new Exam(e));
            }
        }

        public Period GetContainingPeriod(string dayOfWeek, TimeSpan t)
        {
            if(dayOfWeek == "M")
            {
                foreach (Period p in Monday)
                {
                    if (p.StartTime.TimeOfDay <= t && p.EndTime.TimeOfDay >= t)
                    {
                        return p;
                    }
                }
            }
            else if(dayOfWeek == "T")
            {
                foreach (Period p in Tuesday)
                {
                    if (p.StartTime.TimeOfDay <= t && p.EndTime.TimeOfDay >= t)
                    {
                        return p;
                    }
                }

            }
            else if (dayOfWeek == "W")
            {
                foreach (Period p in Wednesday)
                {
                    if (p.StartTime.TimeOfDay <= t && p.EndTime.TimeOfDay >= t)
                    {
                        return p;
                    }
                }
            }
            else if (dayOfWeek == "R")
            {
                foreach (Period p in Thursday)
                {
                    if (p.StartTime.TimeOfDay <= t && p.EndTime.TimeOfDay >= t)
                    {
                        return p;
                    }
                }
            }
            else if (dayOfWeek == "F")
            {
                foreach (Period p in Friday)
                {
                    if (p.StartTime.TimeOfDay <= t && p.EndTime.TimeOfDay >= t)
                    {
                        return p;
                    }
                }
            }

            return null;

        }

        public bool OverlapsWith(Course other)
        {
            foreach(Period p in Monday)
            {
                foreach(Period o in other.Monday)
                {
                    if(p.OverlapsWith(o))
                    {
                        return true;
                    }
                }
            }

            foreach (Period p in Tuesday)
            {
                foreach (Period o in other.Tuesday)
                {
                    if (p.OverlapsWith(o))
                    {
                        return true;
                    }
                }
            }

            foreach (Period p in Wednesday)
            {
                foreach (Period o in other.Wednesday)
                {
                    if (p.OverlapsWith(o))
                    {
                        return true;
                    }
                }
            }

            foreach (Period p in Thursday)
            {
                foreach (Period o in other.Thursday)
                {
                    if (p.OverlapsWith(o))
                    {
                        return true;
                    }
                }
            }

            foreach (Period p in Friday)
            {
                foreach (Period o in other.Friday)
                {
                    if (p.OverlapsWith(o))
                    {
                        return true;
                    }
                }
            }

            foreach (Exam e in Exams)
            {
                foreach (Exam o in other.Exams)
                {
                    if (e.OverlapsWith(o))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
