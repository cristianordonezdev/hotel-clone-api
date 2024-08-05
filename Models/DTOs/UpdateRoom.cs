using System.ComponentModel.DataAnnotations;

namespace hotel_clone_api.Models.DTOs
{
    public class UpdateRoom
    {
        [MinLength(5)]
        [MaxLength(255)]
        public string? Name { get; set; }

        [MinLength(5)]
        [MaxLength(2000)]
        public string? Description { get; set; }

        [MinLength(5)]
        [MaxLength(255)]
        public string? Characteristics { get; set; }

        [Range(100, 500000)]
        public decimal? Price { get; set; }
    }
}
