using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class Exam
    {
        public int ID { get; set; }
        public string ExamNo { get; set; }
        public int CourseID { get; set; }
        public string ExamName { get; set; }
        public int PassScore { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public int ExamType { get; set; }

    }
}
