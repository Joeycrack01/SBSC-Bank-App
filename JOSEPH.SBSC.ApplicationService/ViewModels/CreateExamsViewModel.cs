using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService.ViewModels
{
    public class CreateExamsViewModel
    {
        public string ExamNo { get; set; }
        public int CourseID { get; set; }
        public int PassMark { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public int ExamType { get; set; }
    }
}
