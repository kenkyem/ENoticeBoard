using System;

namespace ENoticeBoard.ViewModels
{
    public class RockFormViewModel
    {
        public int RockId { get; set; }
        public string Subject { get; set; }
        public int Priority { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDue { get; set; }
        public bool Done { get; set; }
    }
}