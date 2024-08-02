using AutoMapper;
using Azure;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;
using hotel_clone_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using hotel_clone_api.Libs;
using System.Text.Json;

namespace hotel_clone_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;
        private readonly ILogger<RoomController> logger;
        private readonly Utils utils;

        public RoomController(IRoomRepository roomRepository, IMapper mapper, 
            IImageRepository imageRepository, IWebHostEnvironment webHostEnvironment, ILogger<RoomController> logger)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
            this.logger = logger;
            this.utils = new Utils(webHostEnvironment);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomsDtos = await roomRepository.GetAllRooms();

            return Ok(roomsDtos);
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid Id)
        {
            var room = await roomRepository.GetById(Id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRoom([FromForm] CreateRoomDto createRoomDto)
        {
            utils.ValidateFileUpload(createRoomDto.File, ModelState);

            if (ModelState.IsValid)
            {
                var roomDomain = mapper.Map<Room>(createRoomDto);

                var roomCreated = await roomRepository.CreateRoom(roomDomain);
                var roomDto = mapper.Map<RoomDetailDto>(roomDomain);

                foreach (var file in createRoomDto.File)
                {
                    var imageDomain = new Image
                    {
                        File = file,
                        // Room Id
                        RelativeRelationId = roomDomain.Id,
                        //Image Type Id
                        ImageTypeId = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                    };
                    var imageUploadedDomain = await imageRepository.UploadImage(imageDomain);
                    if (roomDto.Images == null)
                    {
                        roomDto.Images = new List<ImageDto> {mapper.Map<ImageDto>(imageUploadedDomain) };

                    }
                    else
                    {
                        roomDto.Images.Add(mapper.Map<ImageDto>(imageUploadedDomain));
                    }
                    
                }
                return Ok(roomDto);
            }
            var errorResponse = utils.BuildErrorResponse(ModelState);
            return BadRequest(errorResponse);   
        }

        [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRoom([FromRoute] Guid Id, [FromForm] CreateRoomDto updateRoomDto)
        {
            utils.ValidateFileUpload(updateRoomDto.File, ModelState);

            if (ModelState.IsValid)
            {
                var roomDomain = await roomRepository.UpdateRoom(Id, mapper.Map<Room>(updateRoomDto));
                if (roomDomain == null)
                {
                    return NotFound();
                }

                var roomDto = mapper.Map<RoomDetailDto>(roomDomain);

                foreach (var file in updateRoomDto.File)
                {
                    var imageDomain = new Image
                    {
                        File = file,
                        RelativeRelationId = roomDomain.Id,
                        ImageTypeId = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                    };
                    var imageUploadedDomain = await imageRepository.UploadImage(imageDomain);
                    if (roomDto.Images == null)
                    {
                        roomDto.Images = new List<ImageDto> { mapper.Map<ImageDto>(imageUploadedDomain) };

                    }
                    else
                    {
                        roomDto.Images.Add(mapper.Map<ImageDto>(imageUploadedDomain));
                    }

                }
                return Ok(roomDto);


            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> PatchRoom([FromRoute] Guid Id, [FromBody] JsonPatchDocument<CreateRoomDto> patchDocument)
        {
            var roomDomain = await roomRepository.GetById(Id);
            if (roomDomain == null)
            {
                return NotFound();
            }
            var roomDto = mapper.Map<CreateRoomDto>(roomDomain);
            patchDocument.ApplyTo(roomDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //await roomRepository.PatchRoom(roomDomain, roomDto);
            return Ok(roomDto);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRoom([FromRoute] Guid Id)
        {
            var roomDomain = await roomRepository.DeleteRoom(Id);
            if (roomDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RoomDetailDto>(roomDomain));
        }

    }
}
