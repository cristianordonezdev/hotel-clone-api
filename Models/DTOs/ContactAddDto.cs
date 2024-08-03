using System.ComponentModel.DataAnnotations;

namespace hotel_clone_api.Models.DTOs
{
    public class ContactAddDto
    {
        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(2000)]
        [MinLength(2)]
        public string Message { get; set; }       
    }
}
