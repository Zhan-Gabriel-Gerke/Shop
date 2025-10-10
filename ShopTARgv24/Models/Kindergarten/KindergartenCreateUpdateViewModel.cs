namespace ShopTARgv24.Models.Kindergarten
{
    public class KindergartenCreateUpdateViewModel
    {
        public Guid? KindergartenId { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? TeacherName { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<ImageViewModel> Images { get; set; }
            = new List<ImageViewModel>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}