using System;

namespace ENoticeBoard.ViewModels
{
    public class ObjectFormViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

    }
}