using hotel_clone_api.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace hotel_clone_api.Models.DTOs
{
    public class OfferCreateUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
