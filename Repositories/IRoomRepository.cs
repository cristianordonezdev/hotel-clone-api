﻿using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;

namespace hotel_clone_api.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRooms();
        Task<Room> CreateRoom(Room room, List<IFormFile> files);
        Task<Room?> DeleteRoom(Guid Id);

        Task<Room?> UpdateRoom(Guid Id, Room room, List<IFormFile> files);
        Task<Room?> GetById(Guid Id);

    }
}
