using hotel_clone_api.Models.Domain;

namespace hotel_clone_api.Models.DTOs
{
    public class OfferDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string? imagePath { get; set; }
    }
}
