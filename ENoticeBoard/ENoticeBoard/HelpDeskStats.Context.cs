﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HelpDeskStatsEntities : DbContext
    {
        public HelpDeskStatsEntities()
            : base("name=HelpDeskStatsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Vw_TicketsWithinFinancialWeek> Vw_TicketsWithinFinancialWeek { get; set; }
        public virtual DbSet<ENoticeBoardMstr> ENoticeBoardMstrs { get; set; }
    }
}
