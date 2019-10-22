using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAS.DB.Data;

namespace AggieAutoSchedulerAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly AggieAutoSchedulerContext _context;
        public VerificationController(AggieAutoSchedulerContext context)
        {
            _context = context;
        }

        [HttpGet("[action]/crn={crn}")]
        public bool CRN(int crn)
        {
            return _context.Course.Where(course => course.CRN == crn).Count() == 1;
        }

        [HttpGet("[action]/subject={subject}/courseNumber={courseNumber}")]
        public bool SubjectCourseNumber(string subject, int courseNumber)
        {
            return _context.Course.Where(course => course.Subject.ToUpper() == subject.ToUpper() && course.CourseNumber == courseNumber).Count() > 0;
        }

    }
}