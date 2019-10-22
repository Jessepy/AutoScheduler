using System;
using System.Collections.Generic;
using System.Text;

namespace AAS.ScheduleMaker
{
    public class SubjectCourseSection : SubjectCoursePair
    {
        public int SectionNumber { get; }
        public SubjectCourseSection(string _subject, int _courseNumber, int _sectionNumber) : base(_subject, _courseNumber)
        {
            SectionNumber = _sectionNumber;
        }

        public static bool operator ==(SubjectCourseSection a, SubjectCourseSection b)
        {
            return a.CourseNumber == b.CourseNumber && a.SectionNumber == b.SectionNumber && a.Subject.ToUpper() == b.Subject.ToUpper();
        }

        public static bool operator !=(SubjectCourseSection a, SubjectCourseSection b)
        {
            return a.CourseNumber != b.CourseNumber || a.SectionNumber != b.SectionNumber || a.Subject.ToUpper() != b.Subject.ToUpper();
        }
    }
}
