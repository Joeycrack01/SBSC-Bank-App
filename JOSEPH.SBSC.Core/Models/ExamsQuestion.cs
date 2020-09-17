using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class ExamsQuestion
    {
        public int ID { get; set; }
        public int ExamId { get; set; }
        public int CourseId { get; set; }
        public int CreatedBy { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Marks { get; set; }

    }
}
