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
    
    public partial class Rock
    {
        public int RockId { get; set; }
        public string Subject { get; set; }
        public int Priority { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateDue { get; set; }
        public bool Done { get; set; }
    }
}