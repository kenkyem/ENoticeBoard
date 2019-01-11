using System;
using System.ComponentModel.DataAnnotations;

namespace ENoticeBoard.ViewModels
{
    public class RockFormViewModel
    {
        [Required]
        public int RockId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime DateDue { get; set; }

        
        public DateTime DateDiff { get; set; }
        public bool? Done { get; set; }
        
    }
}