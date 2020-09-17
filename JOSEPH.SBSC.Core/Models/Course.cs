using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseContent { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}
