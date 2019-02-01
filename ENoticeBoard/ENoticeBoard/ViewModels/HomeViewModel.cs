using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class HomeViewModel
    {
        
        public List<Rock> Rocks { get; set; }

        public int DowntimeSum { get; set; }
        
        
        public decimal BreakageSum { get; set; }
        

        public decimal BudgetSum { get; set; }
        
        public List<User> Users { get; set; }
        public User User { get; set; }
        public string BgColorDowntime { get; set; }
        public string BgColorBreakage { get; set; }
        public string BgColorBudget { get; set; }
        public string BgColorOpenTicket { get; set; }
        public int OpenticketSum { get; set; }
    }
}