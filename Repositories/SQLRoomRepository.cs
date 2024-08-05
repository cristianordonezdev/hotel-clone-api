using AutoMapper;
using hotel_clone_api.Data;
using hotel_clone_api.Libs;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml.XPath;

namespace hotel_clone_api.Repositories
{
    public class SQLRoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        private readonly IImageRepository _imageRepository;
        private readonly Utils _utils;

        public SQLRoomRepository(HotelDbContext hotelDbContext,
            IMapper mappe, Utils utils, IImageRepository imageRepository)
        {
            _hotelDbContext = hotelDbContext;
            _utils = utils;
            _imageRepository = imageRepository;
        }

        public async Task<Room> CreateRoom(Room room, List<IFormFile> files)
        {
            await _hotelDbContext.Rooms.AddAsync(room);
            await _hotelDbContext.SaveChangesAsync();

            foreach (var file in files)
            {
                var imageDomain = new Image
                {
                    File = file,
                    RelativeRelationId = room.Id,
                    ImageTypeId = Guid.Parse("3897b275-7a3f-4a84-a620-105b9b0eb89a"),
                };
                var imageUploadedDomain = await _imageRepository.UploadImage(imageDomain);
                if (room.Images == null)
                {
                    room.Images = new List<Image> { imageUploadedDomain };
                }
                else
                {
                    room.Images.Add(imageUploadedDomain);
                }
            }
            return room;
        }

        public async Task<Room?> DeleteRoom(Guid Id)
        {
            var roomDomain = await _hotelDbContext.Rooms.FirstOrDefaultAsync(item => item.Id == Id);
            if (roomDomain == null)
            {
                return null;
            }
           var roomImagesDomain = await _hotelDbContext.Images.Where(item => item.RelativeRelationId == roomDomain.Id).ToListAsync();
            foreach (var image in roomImagesDomain)
            {
                _utils.DeleteImageFromFolder(image.FilePath);
                _hotelDbContext.Images.Remove(image);
            }
            _hotelDbContext.Rooms.Remove(roomDomain);
            await _hotelDbContext.SaveChangesAsync();
            return roomDomain;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            var roomsDomain = await _hotelDbContext.Rooms.ToListAsync();
            List<Guid> roomsIds = roomsDomain.Select(room => room.Id).ToList();
            
      
            var images = await _hotelDbContext.Images
                .Where(i => roomsIds.Contains(i.RelativeRelationId))
                .ToListAsync();


            foreach (var room in roomsDomain)
            {
                var imageFromRoom = images.Where(i => i.RelativeRelationId.Equals(room.Id)).ToList();
                if (imageFromRoom.Count > 0)
                {
                    room.Images = imageFromRoom;
                }
            }
            return roomsDomain;
        }

        public async Task<Room?> GetById(Guid Id)
        {
            var room = await _hotelDbContext.Rooms.FirstOrDefaultAsync(item => item.Id == Id);
            if (room == null)
            {
                return null;
            }
            var images = await _hotelDbContext.Images
                .Where(image => image.RelativeRelationId == room.Id)
                .ToListAsync();

            room.Images = images;
            return room;
        }

        public async Task<Room?> UpdateRoom(Guid Id, Room room)
        {
            var roomDomain = await _hotelDbContext.Rooms.FirstOrDefaultAsync(item => item.Id == Id);

            if (roomDomain == null)
            {
                return null;
            }
            roomDomain.Name = room.Name;
            roomDomain.Description = room.Description;
            roomDomain.Characteristics = room.Characteristics;
            roomDomain.Price = room.Price;

            var images = await _hotelDbContext.Images
                .Where(image => image.RelativeRelationId == roomDomain.Id)
                .ToListAsync();

            roomDomain.Images = images;

            await _hotelDbContext.SaveChangesAsync();

            return roomDomain;
        }

    }
}
