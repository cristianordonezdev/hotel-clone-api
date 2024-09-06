using AutoMapper;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;

namespace hotel_clone_api.Mappings
{
    public class MainAutoMapper : Profile
    {
        public MainAutoMapper()
        {
            // origin - destination
            CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.FilePath).ToList()));
/*            CreateMap<Room, RoomDto>().ReverseMap();
*/            CreateMap<CreateRoomDto, Room>().ReverseMap();
            CreateMap<UpdateRoom, Room>().ReverseMap();
            CreateMap<Room, UpdateRoom>().ReverseMap();

            CreateMap<Room, RoomDetailDto>().ReverseMap();
            CreateMap<RoomDetailDto, Room>().ReverseMap();
            CreateMap<Room, CreateRoomDto>().ReverseMap();

            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<CreateImageDto, Image>().ReverseMap(); 

            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<OfferCreateUpdateDto, Offer>().ReverseMap();
            CreateMap<UpdateOfferDto, Offer>().ReverseMap();
            CreateMap<Offer, OfferDto>().ReverseMap();


            CreateMap<ImageType, ImageTypeDto>().ReverseMap();

            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<ContactAddDto, Contact>().ReverseMap();
        }
    }
}
