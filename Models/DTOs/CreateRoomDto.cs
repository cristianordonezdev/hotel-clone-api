using System.ComponentModel.DataAnnotations;

namespace hotel_clone_api.Models.DTOs
{
    public class CreateRoomDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public List<IFormFile> File { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Characteristics { get; set; }

        [Required]
        [Range(100, 500000)]
        public decimal Price { get; set; }
    }
}
