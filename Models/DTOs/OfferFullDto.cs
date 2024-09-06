using hotel_clone_api.Models.Domain;

namespace hotel_clone_api.Models.DTOs
{
    public class OfferFullDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Image Image { get; set; }
    }
}
