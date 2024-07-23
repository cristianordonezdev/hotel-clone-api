using AutoMapper;
using hotel_clone_api.Data;
using hotel_clone_api.Libs;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace hotel_clone_api.Repositories
{
    public class SQLOfferRepository : IOffersRepository
    {
        private readonly HotelDbContext dbContext;
        private readonly IMapper mapper;
        private readonly Utils utils;

        public SQLOfferRepository(HotelDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.utils = new Utils(webHostEnvironment);
        }
        public async Task<Offer> CreateOffer(Offer offer)
        {
            await dbContext.Offers.AddAsync(offer);
            await dbContext.SaveChangesAsync();
            return offer;
        }

        public async Task<Offer?> DeleteOffer(Guid Id)
        {
            var offerDomain = await dbContext.Offers.FirstOrDefaultAsync(i => i.Id == Id);
            if (offerDomain == null)
            {
                return null;
            }
            var imagesDomain = await dbContext.Images.Where(i => i.RelativeRelationId == offerDomain.Id).ToListAsync();
            foreach (var image in imagesDomain)
            {
                utils.DeleteImageFromFolder(image.FilePath);
                dbContext.Images.Remove(image);
            }
            dbContext.Offers.Remove(offerDomain);
            await dbContext.SaveChangesAsync();

            return offerDomain;
        }

        public async Task<List<OfferDto>> GetOffers()
        {
            var offerDomain = await dbContext.Offers.ToListAsync();
            List<Guid> offersIds= offerDomain.Select(i => i.Id).ToList();
            var allImagesDomain = await dbContext.Images
                .Where(image => offersIds.Contains(image.RelativeRelationId))
                .ToListAsync();

            var offersDto = mapper.Map<List<OfferDto>>(offerDomain);

            foreach (var offer in offersDto)
            {
                var imageFromOffer = allImagesDomain.Where(i => i.RelativeRelationId == offer.Id).ToList();
                if (imageFromOffer.Count > 0)
                {
                    offer.imagePath = imageFromOffer[0].FilePath;
                }
            }
            return offersDto;
        }

        public async Task<Offer?> UpdateOffer(Guid Id, Offer offer)
        {
            var offerDomain = await dbContext.Offers.FirstOrDefaultAsync(i => i.Id == Id);
            if (offerDomain == null)
            {
                return null;
            }
            var imagesDomain = await dbContext.Images.Where(i => i.RelativeRelationId == offerDomain.Id).ToListAsync();

            offerDomain.Name = offer.Name;
            offerDomain.Description = offer.Description;

            foreach (var image in imagesDomain)
            {
                utils.DeleteImageFromFolder(image.FilePath);
            }
            await dbContext.SaveChangesAsync();

            return offerDomain;
        }
    }
}
