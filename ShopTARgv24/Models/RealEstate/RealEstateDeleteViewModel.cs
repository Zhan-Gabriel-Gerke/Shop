namespace ShopTARgv24.Models.RealEstate
{
    public class RealEstateDeleteViewModel
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public List<RealEstateIndexView> Images { get; set; }
            = new List<RealEstateIndexView>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

