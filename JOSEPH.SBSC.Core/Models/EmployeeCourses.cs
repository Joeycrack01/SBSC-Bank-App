using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class EmployeeCourse
    {
        public int ID { get; set; }
        public int CourseId { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateStartedted { get; set; }
        public DateTime DateCompleted { get; set; }


    }
}
