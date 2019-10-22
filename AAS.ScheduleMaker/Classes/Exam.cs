using System;
namespace AAS.ScheduleMaker
{
    public class Exam
    {
        public Exam(AAS.DB.Exam exam)
        {
            Professor = exam.Professor;
            Location = exam.Location;
            StartTime = exam.StartTime;
            EndTime = exam.EndTime;
        }

        public string Professor { get; set; }
        public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool OverlapsWith(Exam other)
        {
            return EndTime >= other.StartTime && other.EndTime >= StartTime; 
        }
    }
}
