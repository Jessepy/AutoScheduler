using System;
using System.Collections.Generic;
using System.Text;

namespace AAS.ScheduleMaker
{
    public class SubjectCoursePair
    {
        public string Subject { get; set; }
        public int CourseNumber { get; set; }
        public SubjectCoursePair(string _subject, int _courseNumber)
        {
            Subject = _subject.ToUpper();
            CourseNumber = _courseNumber;
        }

        public static bool operator ==(SubjectCoursePair a, SubjectCoursePair b)
        {
            return a.CourseNumber == b.CourseNumber && a.Subject.ToUpper() == b.Subject.ToUpper();
        }

        public static bool operator !=(SubjectCoursePair a, SubjectCoursePair b)
        {
            return a.CourseNumber != b.CourseNumber || a.Subject.ToUpper() != b.Subject.ToUpper();
        }
    }
}
