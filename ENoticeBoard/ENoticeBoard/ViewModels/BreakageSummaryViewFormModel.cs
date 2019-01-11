using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class BreakageSummaryViewFormModel
    {
        public List<Vw_BreakagesWithinFinancialPeriod> BreakageWFPs { get; set; }
        public List<GroupByModel> GroupByModels { get; set; }
        public List<DropDownBoxList> Periodddl { get; set; }
        public List<DropDownBoxList> Yearddl { get; set; }
        public string selectedPeriod { get; set; }
        public string selectedYear { get; set; }
      
    }
}