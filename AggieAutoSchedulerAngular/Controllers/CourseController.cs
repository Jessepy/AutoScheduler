using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAS.DB.Data;
using Microsoft.EntityFrameworkCore;
using AAS.ScheduleMaker;
namespace AggieAutoSchedulerAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AggieAutoSchedulerContext _context;
        public CourseController(AggieAutoSchedulerContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/crn={crn}")]
        public object CRNSummary(int crn)
        {
            List<AAS.DB.Course> fetchedCourse = _context.Course.Where(course => course.CRN == crn)
                .Include(course => course.Periods)
                .Include(course => course.Exams).ToList();

            if(fetchedCourse.Count > 0)
            {
                return new Course(fetchedCourse[0]);
            }

            return new { };
        }

        [HttpGet("[action]/subject={subject}/courseNumber={courseNumber}")]
        public List<object> SubjectCourseNumberSummary(string subject, int courseNumber)
        {
            List<object> toReturn = new List<object>();
            List<AAS.DB.Course> courses = _context.Course.Where(course =>
            course.Subject.ToUpper() == subject.ToUpper() && course.CourseNumber == courseNumber
            ).ToList();

            foreach(AAS.DB.Course course in courses)
            {
                toReturn.Add(new {
                    course.CRN
                });
            }

            return toReturn;
        }


         
    }


}