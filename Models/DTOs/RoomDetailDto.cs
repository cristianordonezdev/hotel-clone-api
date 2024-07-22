namespace hotel_clone_api.Models.DTOs
{
    public class RoomDetailDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Characteristics { get; set; }

        public decimal Price { get; set; }

        public List<ImageDto>? Images { get; set; }
    }
}
