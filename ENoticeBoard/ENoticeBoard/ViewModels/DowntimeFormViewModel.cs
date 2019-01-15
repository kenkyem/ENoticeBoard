using System;
using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class DowntimeFormViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int Type { get; set; }
        public IEnumerable<Downtimetype> Types { get; set; }
        public string Status { get; set; }
    }
}