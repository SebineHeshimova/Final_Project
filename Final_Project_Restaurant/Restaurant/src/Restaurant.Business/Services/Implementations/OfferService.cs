using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.OfferExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Implementations
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _repository;
        private readonly IWebHostEnvironment _env;
        public OfferService(IOfferRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task CreateAsync(Offer offer)
        {
            if (offer == null) throw new OfferNullException("Entity cannot be null!");
            if (offer.ImageFile != null)
            {
                if (offer.ImageFile.ContentType != "image/jpeg" && offer.ImageFile.ContentType != "image/png")
                {
                    throw new OfferImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (offer.ImageFile.Length > 2097152)
                {
                    throw new OfferImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                offer.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/offers", offer.ImageFile);
            }
            offer.CreatedDate = DateTime.UtcNow.AddHours(4);
            offer.UpdatedDate = DateTime.UtcNow.AddHours(4);
            offer.IsDeleted = false;
            await _repository.CreateAsync(offer);
            await _repository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var offer = await _repository.SingleAsync(o => o.Id == id);
            if (offer == null) throw new OfferNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/offers", offer.ImageUrl);
            _repository.Delete(offer);
            await _repository.CommitAsync();
        }

        public async Task<List<Offer>> GetAllAsync(Expression<Func<Offer, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public async Task<Offer> GetByIdAsync(Expression<Func<Offer, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.SingleAsync(expression, includes);
        }

        public async Task SoftDelete(int id)
        {
            var offer=await _repository.SingleAsync(o=>o.Id==id);
            if (offer == null) throw new OfferNullException("Entity cannot be null!");
            offer.IsDeleted=!offer.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(Offer offer)
        {
            var existOffer=await _repository.SingleAsync(o=>o.Id==offer.Id);
            if (offer == null) throw new OfferNullException("Entity cannot be null!");
            if (offer.ImageFile != null)
            {
                if (offer.ImageFile.ContentType != "image/jpeg" && offer.ImageFile.ContentType != "image/png")
                {
                    throw new OfferImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (offer.ImageFile.Length > 2097152)
                {
                    throw new OfferImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/offers", existOffer.ImageUrl);
                existOffer.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/offers", offer.ImageFile);
            }
            existOffer.UpdatedDate = DateTime.UtcNow.AddHours(4);
            existOffer.Title= offer.Title;
            existOffer.SubTitle= offer.SubTitle;
            existOffer.Description= offer.Description;
            existOffer.RedirectText= offer.RedirectText;
            existOffer.RedirectUrl= offer.RedirectUrl;
            await _repository.CommitAsync();
        }
    }
}
