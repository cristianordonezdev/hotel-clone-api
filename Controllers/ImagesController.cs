using AutoMapper;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;
using hotel_clone_api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hotel_clone_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;

        public ImagesController(IImageRepository imageRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? type_image)
        {
            var imagesDomain = await imageRepository.GetAllImages(type_image);
            return Ok(mapper.Map<List<ImageDto>>(imagesDomain));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var image = await imageRepository.DeleteImage(Id);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UploadImage([FromForm] CreateImageDto createImageDto)
        {
            List<Image> imagesUploaded = new List<Image> { };
            foreach (var image in createImageDto.File)
            {
                Image newImage = new Image
                {
                    File = image,
                    ImageTypeId = createImageDto.ImageTypeId
                };
                var imageUploaded = await imageRepository.UploadImage(newImage);
                imagesUploaded.Add(imageUploaded);
            }
          
            return Ok(mapper.Map<List<ImageDto>>(imagesUploaded));
        }

        [HttpGet]
        [Authorize(Roles = "Writer")]
        [Route("Types")]
        public async Task<IActionResult> GetAllImagestype()
        {
            var imagesTypesDomain = await imageRepository.GetAllTypesImages();
            return Ok(mapper.Map<List<ImageTypeDto>>(imagesTypesDomain));
        }
    }
}
