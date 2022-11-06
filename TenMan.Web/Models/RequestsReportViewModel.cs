using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class RequestsReportViewModel
    {
        public int TotalCant { get; set; }
        public int InProcessCant { get; set; }
        public int FinishedCant { get; set; }
        public int AsignedCant { get; set; }
        public int GeneratedCant { get; set; }

        public IEnumerable<Request> Requests { get; set; }
    }
}
