namespace ShopTARgv24.Models.Kindergarten
{
    public class KindergartenDeleteViewModel
    {
        public Guid? KindergartenId { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }
        public List<KindergartenIndexView> Images { get; set; }
            = new List<KindergartenIndexView>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

