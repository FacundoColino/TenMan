using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
    public class ListCostsViewModel
    {
        public int CommitteeId { get; set; }
        public IEnumerable<Data.Entities.Field> Fields { get; set; }
        public IEnumerable<Cost> Costs { get; set; }
    }
}
