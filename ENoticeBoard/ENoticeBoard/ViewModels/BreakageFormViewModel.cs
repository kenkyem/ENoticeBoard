using System;
using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class BreakageFormViewModel
    {

        public int Id { get; set; }
        public string Subject { get; set; }

        public DateTime Date { get; set; }
        public decimal Cost { get; set; }

        public IEnumerable<Site> Sites { get; set; }
        public int SiteId { get; set; }
        
        
        public IEnumerable<BreakageType> Types { get; set; }
        public int TypeId { get; set; }
        
    }
}