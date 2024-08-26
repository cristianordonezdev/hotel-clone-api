using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace hotel_clone_api.Models.DTOs
{
    public class CreateImageDto
    {
        [Required]
        public List<IFormFile> File { get; set; }

        [Required]
        public Guid ImageTypeId { get; set; }

        public Guid RelativeRelationId { get; set; }

    }
}
