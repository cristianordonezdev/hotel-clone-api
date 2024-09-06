using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;

namespace hotel_clone_api.Repositories
{
    public interface IOffersRepository
    {
        Task<Offer> CreateOffer(Offer offer);

        Task<Offer?> UpdateOffer(Guid Id, Offer offer);

        Task<Offer?> DeleteOffer(Guid Id);

        Task<List<OfferDto>> GetOffers();
        Task<OfferFullDto?> GetOffer(Guid Id);

    }
}
