using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class UserCertificateRecord
    {
        public int ID { get; set; }
        public int ExamId { get; set; }
        public int CourseId { get; set; }
        public int UserID { get; set; }
        public string CertificateNo { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
