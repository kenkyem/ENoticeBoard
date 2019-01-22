using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class HomeViewModel
    {
        
        public List<Rock> Rocks { get; set; }

        public int DowntimeSum { get; set; }
        public Target DowntimeTargetPlanned { get; set; }
        public Target DowntimeTargetUnplanned { get; set; }
        
        public decimal BreakageSum { get; set; }
        public Target BreakageTarget { get; set; }

        public decimal BudgetSum { get; set; }
        public Target BudgetTarget { get; set; }

        public List<User> Users { get; set; }
        public User User { get; set; }
        public bool OnTarget(decimal target, decimal actual)
        {
            return target > actual;
        }
    }
}