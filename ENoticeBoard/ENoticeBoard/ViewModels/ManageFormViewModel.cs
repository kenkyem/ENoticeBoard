using System.Collections.Generic;

namespace ENoticeBoard.ViewModels
{
    public class ManageFormViewModel
    {
        public List<User> Users { get; set; }
        public List<BreakageType> BreakageTypes { get; set; }
        public List<Downtimetype> Downtimetypes { get; set; }
        public List<Target> Targets { get; set; }
    }
}