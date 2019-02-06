using System;

namespace ENoticeBoard.ViewModels
{
    public class TicketFormViewModel
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Category { get; set; }
        public string Assignedto { get; set; }
    }
}