using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AAS.DB
{
    public class Period
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public string Professor { get; set; }
        public string Location { get; set; }
        public string DayOfWeek { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
    }
}
