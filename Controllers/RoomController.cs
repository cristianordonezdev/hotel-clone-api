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
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<RoomController> _logger;
        private readonly Utils _utils;

        public RoomController(IRoomRepository roomRepository, IMapper mapper, 
            IImageRepository imageRepository, Utils utils, ILogger<RoomController> logger)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
            _logger = logger;
            _utils = utils;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomDomains = await _roomRepository.GetAllRooms();
            var roomsDtos = _mapper.Map<List<RoomDto>>(roomDomains);
            return Ok(roomsDtos);
        }
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid Id)
        {
            var room = await _roomRepository.GetById(Id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RoomDto>(room));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateRoom([FromForm] CreateRoomDto createRoomDto)
        {
            _utils.ValidateFileUpload(createRoomDto.File, ModelState);

            if (ModelState.IsValid)
            {
                var roomDomain = _mapper.Map<Room>(createRoomDto);
                var roomCreated = await _roomRepository.CreateRoom(roomDomain, createRoomDto.File);
                var roomDto = _mapper.Map<RoomDto>(roomCreated);

                return Ok(roomDto);
            }
            var errorResponse = _utils.BuildErrorResponse(ModelState);
            return BadRequest(errorResponse);   
        }

       [HttpPut]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateRoom([FromRoute] Guid Id, [FromForm] CreateRoomDto updateRoomDto)
        {
            _utils.ValidateFileUpload(updateRoomDto.File, ModelState);

            if (ModelState.IsValid)
            {
                var roomDomain = await _roomRepository.UpdateRoom(Id, _mapper.Map<Room>(updateRoomDto), updateRoomDto.File);
                if (roomDomain == null)
                {
                    return NotFound();
                }

                var roomDto = _mapper.Map<RoomDto>(roomDomain);

                return Ok(roomDto);
            }
            var errorResponse = _utils.BuildErrorResponse(ModelState);

            return BadRequest(errorResponse);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRoom([FromRoute] Guid Id)
        {
            var roomDomain = await _roomRepository.DeleteRoom(Id);
            if (roomDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RoomDto>(roomDomain));
        }

    }
}
