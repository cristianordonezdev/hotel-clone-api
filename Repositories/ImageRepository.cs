using hotel_clone_api.Data;
using hotel_clone_api.Libs;
using hotel_clone_api.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hotel_clone_api.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly HotelDbContext hotelDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Utils utils;

        public ImageRepository(HotelDbContext hotelDbContext, IWebHostEnvironment webHostEnvironment,
             IHttpContextAccessor httpContextAccessor)
        {
            this.hotelDbContext = hotelDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.utils = new Utils(webHostEnvironment);
        }

        public async Task<Image?> DeleteImage(Guid Id)
        {
            var imageDomain = await hotelDbContext.Images.FirstOrDefaultAsync(item => item.Id == Id);
            if (imageDomain == null)
            {
                return null;
            }
            utils.DeleteImageFromFolder(imageDomain.FilePath);
          
            hotelDbContext.Images.Remove(imageDomain);
            await hotelDbContext.SaveChangesAsync();

            return imageDomain;
        }

        public async Task<Image> UploadImage(Image image)
        {
            string unique_name = GenerateUniqueFileName();
            string extensionImage = Path.GetExtension(image.File.FileName);
            // store files in our app... images folder
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{unique_name}{extensionImage}");

            //Upload Image to local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);


            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{unique_name}{extensionImage}";
            image.FilePath = urlFilePath;

            // Add Image to the images table
            await hotelDbContext.Images.AddAsync(image);
            await hotelDbContext.SaveChangesAsync();

            return image;
        }
        private string GenerateUniqueFileName()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return timestamp;
        }

        public async Task<List<Image>> GetAllImages(string? type_image)
        {
            //var typeImageId = await hotelDbContext.ImageTypes.FirstOrDefaultAsync(item => item.Type == "room");
            //var imagesDomain = await hotelDbContext.Images.Where(item => item.ImageTypeId == typeImageId).ToListAsync();
            if (!string.IsNullOrEmpty(type_image))
            {
                var imagesDomain = await hotelDbContext.Images
                    .Join(
                        hotelDbContext.ImageTypes,
                        image => image.ImageTypeId,
                        imageType => imageType.Id,
                        (image, imageType) => new { Image = image, ImageType = imageType }
                    )
                    .Where(joinResult => joinResult.ImageType.Type == type_image)
                    .Select(joinResult => joinResult.Image)
                    .ToListAsync();
                return imagesDomain;
            } else
            {
                var imagesDomain = await hotelDbContext.Images
                    .Join(
                        hotelDbContext.ImageTypes,
                        image => image.ImageTypeId,
                        imageType => imageType.Id,
                        (image, imageType) => new { Image = image, ImageType = imageType }
                    )
                    .Select(joinResult => joinResult.Image)
                    .ToListAsync();
                
                return imagesDomain;
            }

        }

        public async Task<List<ImageType>> GetAllTypesImages()
        {
            var imagesTypeDomain = await hotelDbContext.ImageTypes.ToListAsync();
            return imagesTypeDomain;
        }
    }
}
