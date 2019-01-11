﻿using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class DowntimeSummaryViewModel
    {
        public List<Vw_DowntimesWithinFinancialPeriod> DowntimeWFPs { get; set; }
        public List<GroupByModel> GroupByModels { get; set; }
        public List<DropDownBoxList> Periodddl { get; set; }
        public List<DropDownBoxList> Yearddl { get; set; }
        public string SelectedPeriod { get; set; }
        public string SelectedYear { get; set; }
    }
}