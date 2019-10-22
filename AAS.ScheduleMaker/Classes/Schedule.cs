using System;
using System.Collections.Generic;
using System.Text;

namespace AAS.ScheduleMaker
{
    public class Schedule : IComparable<Schedule>
    {
        public List<Course> Courses { get; set; }

        public int Score { get; set; }
        public Schedule()
        {
            Courses = new List<Course>();
        }

        public bool FitsInSchedule(Course toCheck)
        {
            foreach(Course c in Courses)
            {
                if(c.OverlapsWith(toCheck))
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasConflict
        {
            get
            {
                for(int i = 0; i < Courses.Count; i++)
                {
                    for(int j = i + 1; j < Courses.Count; j++)
                    {
                        if(Courses[i].OverlapsWith(Courses[j]))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append(this.GetHashCode());
            b.Append(" ");
            foreach(Course c in Courses)
            {
                b.Append(c.CRN);
                b.Append(" ");
            }

            return b.ToString();
        }


        public int CompareTo(Schedule other)
        {
            return Score - other.Score;
        }
    }
}
