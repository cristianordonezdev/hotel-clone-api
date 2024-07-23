using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;

namespace hotel_clone_api.Repositories
{
    public interface IRoomRepository
    {
        Task<List<RoomDto>> GetAllRooms();
        Task<RoomDetailDto?> GetById(Guid Id);
        Task<Room> CreateRoom(Room room);

        Task<Room?> UpdateRoom(Guid Id, Room room);

        Task<Room?> DeleteRoom(Guid Id);

        Task<Room> PatchRoom(Room room, CreateRoomDto createRoomDto);

    }
}
