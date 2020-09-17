using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class EmployeeGrade
    {
        public int ID { get; set; }
        public int ExamId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public int GradeScore { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
