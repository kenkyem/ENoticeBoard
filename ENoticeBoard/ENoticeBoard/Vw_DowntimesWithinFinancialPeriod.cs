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
    
    public partial class Vw_DowntimesWithinFinancialPeriod
    {
        public System.DateTime Date { get; set; }
        public string FinancialPeriod { get; set; }
        public string FinancialYear { get; set; }
        public int DownTimeId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool isDeleted { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
