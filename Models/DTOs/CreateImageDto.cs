using System.ComponentModel.DataAnnotations;

namespace hotel_clone_api.Models.DTOs
{
    public class CreateImageDto
    {
        [Required]
        public List<IFormFile> File { get; set; }

        [Required]
        public Guid ImageTypeId { get; set; }
    }
}
