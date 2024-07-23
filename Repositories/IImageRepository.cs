
using hotel_clone_api.Models.Domain;

namespace hotel_clone_api.Repositories
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllImages(string? type_image);
        Task<Image?> DeleteImage(Guid Id);
        Task<Image> UploadImage(Image image);
        Task<List<ImageType>> GetAllTypesImages();
    }
}
