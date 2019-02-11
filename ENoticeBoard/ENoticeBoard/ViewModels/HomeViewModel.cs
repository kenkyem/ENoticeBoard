using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class HomeViewModel
    {
        
        public List<Rock> Rocks { get; set; }


        //Panel
        public int DowntimeSum { get; set; }
        public decimal BreakageSum { get; set; }
        public decimal BudgetSum { get; set; }
        
        //LeaderBoard
        public List<GroupByMember> TicketsThisWeek { get; set; }
        public List<GroupByMember> TicketsPriorWeek { get; set; }

        //Calender
        public List<User> Users { get; set; }
        public User User { get; set; }
        public string BgColorDowntime { get; set; }
        public string BgColorBreakage { get; set; }
        public string BgColorBudget { get; set; }
        public string BgColorOpenTicket { get; set; }
        public int OpenticketSum { get; set; }
        public int TotalClosedTicketsThisWeek { get; set; }
        public int TotalClosedTicketsLastWeek { get; set; }
    }
}