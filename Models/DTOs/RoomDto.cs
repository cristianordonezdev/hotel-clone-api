using hotel_clone_api.Models.Domain;

namespace hotel_clone_api.Models.DTOs
{
    public class RoomDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Characteristics { get; set; }

        public string? Image { get; set; }

        public decimal Price { get; set; }

        public List<string> Images { get; set; }
    }
}
