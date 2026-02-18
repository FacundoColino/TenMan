using System;

namespace TenMan.Web.Data.Entities.Settings
{
    public enum HeaderAlignment
    {
        Left = 1,
        Center = 2,
        Right = 3
    }
    public class ExpenseSummarySettings
    {
        public int Id { get; set; }

        public int CommitteeId { get; set; }
        public Committee Committee { get; set; }
        public HeaderAlignment HeaderAlignment { get; set; }

        public bool ShowUnitDetails { get; set; }
        public bool ShowPercentages { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
