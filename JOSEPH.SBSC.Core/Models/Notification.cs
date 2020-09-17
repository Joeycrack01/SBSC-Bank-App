using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Core.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
    }
}
