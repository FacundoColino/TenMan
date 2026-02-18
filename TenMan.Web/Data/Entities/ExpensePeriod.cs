using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TenMan.Web.Models;

namespace TenMan.Web.Data.Entities
{
    public class ExpensePeriod
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => Date.ToLocalTime();

        public bool Current { get; set; }

        public int CommitteeId { get; set; }
        public ICollection<UnitDescriptionLine> UnitDescriptionLines { get; set; }

        public string HtmlSnapshot { get; set; }
    }
       
}
