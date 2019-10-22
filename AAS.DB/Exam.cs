using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AAS.DB
{
    public class Exam
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public string Professor { get; set; }
        public string Location { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

    }
}
