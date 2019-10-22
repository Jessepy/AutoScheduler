using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AAS.DB.Data;
using AAS.ScheduleMaker;

namespace AggieAutoSchedulerAngular.Controllers
{
    [Route("api/[controller]")]
    public class AutocompleteController : Controller
    {
        private readonly AggieAutoSchedulerContext _context;
        public AutocompleteController(AggieAutoSchedulerContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/crn={crn}")]
        public List<int> CRN(int crn)
        {
            List<AAS.DB.Course> courses = _context.Course.Where((course) => 
            FirstCharactersMatching(course.CRN.ToString(), crn.ToString())
            ).ToList();
            
            List<int> toReturn = new List<int>();
            foreach (AAS.DB.Course c in courses)
            {
                if (!toReturn.Contains(c.CRN))
                {
                    toReturn.Add(c.CRN);
                }
            }

            return toReturn;
        }

        [HttpGet("[action]/subject={subject}/courseNumber={courseNumber}")]
        public List<SubjectCoursePair> SubjectCourseNumber(string subject, int courseNumber)
        {

            List<AAS.DB.Course> validCourses = _context.Course.Where(course =>
                    FirstCharactersMatching(course.Subject, subject) && 
                    (courseNumber == -1 || FirstCharactersMatching(course.CourseNumber.ToString(), courseNumber.ToString()))
                ).ToList();

            List<SubjectCoursePair> toReturn = new List<SubjectCoursePair>();

            foreach (AAS.DB.Course c in validCourses)
            {
                SubjectCoursePair toAdd = new SubjectCoursePair(c.Subject, c.CourseNumber);
                if (!toReturn.Any(scPair => scPair.Subject == c.Subject && scPair.CourseNumber == c.CourseNumber))
                {
                    toReturn.Add(toAdd);
                }
            }

            return toReturn;
        }

        private bool FirstCharactersMatching(string string1, string string2)
        {
            return string1.ToUpper().IndexOf(string2.ToUpper()) == 0;
        }
    }
}
