using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class RockFormViewModel
    {
        
        public List<Rock> Rocks { get; set; }
        public List<DropDownBoxList> Monthddl { get; set; }
        public List<DropDownBoxList> Yearddl { get; set; }
        public string selectedMonth { get; set; }
        public string selectedYear { get; set; }
    }
}