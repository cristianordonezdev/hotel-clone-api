﻿using AutoMapper;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;

namespace hotel_clone_api.Mappings
{
    public class MainAutoMapper : Profile
    {
        public MainAutoMapper()
        {
            // origin - destination
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<CreateRoomDto, Room>().ReverseMap();
            CreateMap<Room, RoomDetailDto>().ReverseMap();
            CreateMap<RoomDetailDto, Room>().ReverseMap();
            CreateMap<Room, CreateRoomDto>().ReverseMap();

            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<CreateImageDto, Image>().ReverseMap(); 

            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<OfferCreateUpdateDto, Offer>().ReverseMap();

            CreateMap<ImageType, ImageTypeDto>().ReverseMap();
        }
    }
}