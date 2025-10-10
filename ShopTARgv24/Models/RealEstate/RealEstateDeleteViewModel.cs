using ShopTARgv24.Models.Kindergarten;

namespace ShopTARgv24.Models.RealEstate
{
    public class RealEstateDeleteViewModel
    {
        public Guid? Id { get; set; }
        public double? Area { get; set; }
        public string? Location { get; set; }
        public int? RoomNumber { get; set; }
        public string? BuildingType { get; set; }
        public List<Kindergarten.KindergartenIndexView> Images { get; set; }
            = new List<Kindergarten.KindergartenIndexView>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

