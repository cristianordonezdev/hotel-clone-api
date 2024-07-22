using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_clone_api.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FilePath { get; set; }
        public Guid RelativeRelationId { get; set; }
        public Guid ImageTypeId { get; set; }

    }
}
