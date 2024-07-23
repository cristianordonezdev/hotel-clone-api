using AutoMapper;
using hotel_clone_api.Data;
using hotel_clone_api.Libs;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace hotel_clone_api.Repositories
{
    public class SQLRoomRepository : IRoomRepository
    {
        private readonly HotelDbContext hotelDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        private readonly Utils utils;

        public SQLRoomRepository(HotelDbContext hotelDbContext, IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
        {
            this.hotelDbContext = hotelDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.utils = new Utils(webHostEnvironment);
        }

        public async Task<Room> CreateRoom(Room room)
        {
            await hotelDbContext.Rooms.AddAsync(room);
            await hotelDbContext.SaveChangesAsync();
            return room;
        }

        public async Task<Room?> DeleteRoom(Guid Id)
        {
            var roomDomain = await hotelDbContext.Rooms.FirstOrDefaultAsync(item => item.Id == Id);
            if (roomDomain == null)
            {
                return null;
            }
            var roomImagesDomain = await hotelDbContext.Images.Where(item => item.RelativeRelationId == roomDomain.Id).ToListAsync();
            foreach (var image in roomImagesDomain)
            {
                utils.DeleteImageFromFolder(image.FilePath);
                hotelDbContext.Images.Remove(image);
            }
            hotelDbContext.Rooms.Remove(roomDomain);
            await hotelDbContext.SaveChangesAsync();
            return roomDomain;
        }

        public async Task<List<RoomDto>> GetAllRooms()
        {
            var rooms = await hotelDbContext.Rooms.ToListAsync();
            var roomsDto = mapper.Map<List<RoomDto>>(rooms);
            List<Guid> roomsIds = rooms.Select(room => room.Id).ToList();

            var images = await hotelDbContext.Images
                .Where(i => roomsIds.Contains(i.RelativeRelationId))
                .ToListAsync();


            foreach (var room in roomsDto)
            {
                var imageFromRoom = images.Where(i => i.RelativeRelationId.Equals(room.Id)).ToList();
                if (imageFromRoom.Count > 0)
                {
                    room.Image = imageFromRoom[0].FilePath;
                }
            }
            return roomsDto;
        }

        public async Task<RoomDetailDto?> GetById(Guid Id)
        {
            var room = await hotelDbContext.Rooms.FirstOrDefaultAsync(item => item.Id == Id);
            if (room == null)
            {
                return null;
            }
            var images = await hotelDbContext.Images
                .Where(image => image.RelativeRelationId == room.Id)
                .ToListAsync();

            RoomDetailDto roomDto = mapper.Map<RoomDetailDto>(room);
            roomDto.Images = mapper.Map<List<ImageDto>>(images);

            return roomDto;
        }

        public async Task<Room?> UpdateRoom(Guid Id, Room room)
        {
            var roomDomain = await hotelDbContext.Rooms.FirstOrDefaultAsync(item => item.Id == Id);
            var imagesDomain = await hotelDbContext.Images
                .Where(images => images.RelativeRelationId == roomDomain.Id)
                .ToListAsync();
            if (roomDomain == null)
            {
                return null;
            }
            roomDomain.Name = room.Name;
            roomDomain.Description = room.Description;
            roomDomain.Characteristics = room.Characteristics;
            roomDomain.Price = room.Price;
            foreach (var image in imagesDomain)
            {
                utils.DeleteImageFromFolder(image.FilePath);
                hotelDbContext.Images.Remove(image);
            }
            
            await hotelDbContext.SaveChangesAsync();

            return roomDomain;
        }

        public async Task<Room> PatchRoom(Room room, CreateRoomDto createRoomDto)
        {
            room.Name = createRoomDto.Name;
            room.Description = createRoomDto.Description;
            room.Characteristics = createRoomDto.Characteristics;
            room.Price = createRoomDto.Price;

            await hotelDbContext.SaveChangesAsync();
            return room;
        }

    }
}
