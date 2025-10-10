namespace ShopTARgv24.Models.Kindergarten
{
    public class KindergartenDetailsViewModel
    {
        public Guid? KindergartenId { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }
        public List<ImageViewModel> Image { get; set; }
            = new List<ImageViewModel>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}