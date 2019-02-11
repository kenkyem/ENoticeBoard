namespace ENoticeBoard.ViewModels
{
    public class GroupByModel
    {
        public string Period { get; set; }
        public string Year { get; set; }
        public decimal Sum { get; set; }
    }

    public class GroupByMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Sum { get; set; }
    }
}