//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENoticeBoard
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vw_TicketsWithinFinancialWeek
    {
        public string FinancialWeek { get; set; }
        public string FinancialPeriod { get; set; }
        public string FinancialYear { get; set; }
        public Nullable<System.DateTime> ClosedAt { get; set; }
        public int ENoticeBoardId { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string Summary { get; set; }
        public Nullable<bool> CurrentWeek { get; set; }
        public Nullable<bool> PriorWeek { get; set; }
        public Nullable<bool> CurrentPeriod { get; set; }
        public Nullable<bool> PriorPeriod { get; set; }
    }
}
