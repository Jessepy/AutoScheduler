using System.Data.Odbc;
using System;
using System.Collections.Generic;
using AAS.DB;
namespace AAS.DB.Data
{
    public class TransferRecordes
    {
        public static void TransferRecords(AggieAutoSchedulerContext _context)
        {
            OdbcConnection connection = new OdbcConnection(@"Driver={Microsoft Access Driver (*.mdb, *.accdb)};DBQ=C:\Users\Jesse Sprinkle\Documents\Access Database\CourseDatabase.accdb");
            connection.Open();
            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT CRN FROM COURSES";

            OdbcDataReader reader = command.ExecuteReader();
            List<int> crns = new List<int>();
            while(reader.Read())
            {
                crns.Add(reader.GetInt32(0));
            }


            foreach(int crn in crns)
            {
                OdbcCommand courseCommand = connection.CreateCommand();
                courseCommand.CommandText = "SELECT * FROM COURSES WHERE CRN=" + crn;
                OdbcDataReader courseReader = courseCommand.ExecuteReader();
                Course c = new Course();
                List<string> days = new List<string>();
                List<string> instructors = new List<string>();
                List<string> locations = new List<string>();
                List<DateTime> startTimes = new List<DateTime>();
                List<DateTime> endTimes = new List<DateTime>();
                List<DateTime> startDates = new List<DateTime>();
                List<DateTime> endDates = new List<DateTime>();
                if(courseReader.Read())
                {
                    c.CRN = crn;
                    c.Title = courseReader.GetString(2);
                    c.Credits = courseReader.GetInt32(3);
                    c.Subject = courseReader.GetString(4);
                    c.CourseNumber = courseReader.GetInt32(5);
                    c.SectionNumber = courseReader.GetInt32(6);
                    instructors.Add(courseReader.GetString(7));
                    startTimes.Add(courseReader.GetDateTime(8));
                    endTimes.Add(courseReader.GetDateTime(9));
                    days.Add(courseReader.GetString(10));
                    locations.Add(courseReader.GetString(11));
                    startDates.Add(courseReader.GetDateTime(12));
                    endDates.Add(courseReader.GetDateTime(13));

                }
                else
                {
                    continue;
                }

                while (courseReader.Read())
                {
                    instructors.Add(courseReader.GetString(7));
                    startTimes.Add(courseReader.GetDateTime(8));
                    endTimes.Add(courseReader.GetDateTime(9));
                    days.Add(courseReader.GetString(10));
                    locations.Add(courseReader.GetString(11));
                    startDates.Add(courseReader.GetDateTime(12));
                    endDates.Add(courseReader.GetDateTime(13));
                }

                for(int i = 0; i < days.Count; i++)
                {
                    if(startDates[i] == endDates[i])
                    {
                        Exam e = new Exam();
                        e.Location = locations[i];
                        e.Professor = instructors[i];
                        e.StartTime = startDates[i] + startTimes[i].TimeOfDay;
                        e.EndTime = endDates[i] + endTimes[i].TimeOfDay;
                        c.Exams.Add(e);                        
                    }
                    else
                    {
                        for(int j = 0; j < days[i].Length; j++)
                        {
                            Period p = new Period();
                            p.DayOfWeek = days[i][j] + "";
                            p.Professor = instructors[i];
                            p.Location = locations[i];
                            p.StartTime = startTimes[i];
                            p.EndTime = endTimes[i];
                            c.Periods.Add(p);
                        }
                    }
                }
                _context.Course.Add(c);
                _context.SaveChanges();


            }

            connection.Close();
        }
    }
}
