using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AAS.DB
{
    public class Course
    {
        public Course()
        {
            Periods = new List<Period>();
            Exams = new List<Exam>();
        }

        [Key]
        public int ID { get; set; }
        public int CRN { get; set; }
        public string Subject { get; set; }
        public int Credits { get; set; }
        public string Title { get; set; }
        public int CourseNumber { get; set; }
        public int SectionNumber { get; set; }
        public List<Period> Periods { get; set; }
        public List<Exam> Exams { get; set; }

    }
}
