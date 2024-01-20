namespace Apartments.Application.Dtos
{
    public class ApartmentDto
    {
        public string? ApartmentId { get; set; }
        public string LandlordId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Area { get; set; }
        public string Telephone { get; set; }
    }

}
