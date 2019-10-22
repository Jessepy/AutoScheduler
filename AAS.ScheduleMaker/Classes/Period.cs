using System;

namespace AAS.ScheduleMaker
{
    public class Period
    {
        public Period(AAS.DB.Period period)
        {
            DayOfWeek = period.DayOfWeek.ToUpper();
            Professor = period.Professor;
            Location = period.Location;
            StartTime = period.StartTime;
            EndTime = period.EndTime;
        }

        public string DayOfWeek { get; set; }
        public string Location { get; set; }
        public string Professor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public bool OverlapsWith(Period other)
        {
            return DayOfWeek == other.DayOfWeek && EndTime >= other.StartTime && other.EndTime >= StartTime;
        }

    }
}
