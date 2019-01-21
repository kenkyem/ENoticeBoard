using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public partial class RockSummaryViewModel
    {
        public List<Rock> Rocks { get; set; }
        public List<Vw_RocksWithinFinancialPeriod> RockWFPs { get; set; }
        public List<GroupByModel> GroupByModels { get; set; }
        public List<DropDownBoxList> Periodddl { get; set; }
        public List<DropDownBoxList> Yearddl { get; set; }
        public string SelectedPeriod { get; set; }
        public string SelectedYear { get; set; }
    }
}