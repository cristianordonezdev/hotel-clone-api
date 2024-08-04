using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_clone_api.Models.Domain
{
    public class Room
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Characteristics { get; set; }

        public decimal Price { get; set; }

        [NotMapped]
        public List<Image> Images { get; set; }
    }
}