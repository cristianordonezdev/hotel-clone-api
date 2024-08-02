using hotel_clone_api.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace hotel_clone_api.Models.DTOs
{
    public class OfferCreateUpdateDto
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
        public IFormFile File { get; set; }
    }
}
