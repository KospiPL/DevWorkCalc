using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.W.C.Lib.D.W.C.Models.App
{
    public class TaskReport
    {
        public int TaskId { get; set; }
        public string Project { get; set; }
        public string AssignedPerson { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double WorkTime { get; set; }
    }
}
