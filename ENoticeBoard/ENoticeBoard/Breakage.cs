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
    
    public partial class Breakage
    {
        public int BreakageId { get; set; }
        public string Subject { get; set; }
        public System.DateTime Date { get; set; }
        public int Site { get; set; }
        public decimal Cost { get; set; }
        public int Type { get; set; }
        public bool isDeleted { get; set; }
    
        public virtual Site Site1 { get; set; }
        public virtual BreakageType BreakageType1 { get; set; }
    }
}
