using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.FeedbackExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;
        private readonly IWebHostEnvironment _env;
        public FeedbackService(IFeedbackRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task CreateAsync(Feedback feedback)
        {
            if (feedback.ImageFile != null)
            {
                if (feedback.ImageFile.ContentType != "image/jpeg" && feedback.ImageFile.ContentType != "image/png")
                {
                    throw new FeedbackImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (feedback.ImageFile.Length > 2097152)
                {
                    throw new FeedbackImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                feedback.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/feedbacks", feedback.ImageFile);
            }
            else
            {
                throw new FeedbackNullException("ImageFile", "Entity cannot be null!");
            }
            feedback.CreatedDate = DateTime.UtcNow.AddHours(4);
            feedback.UpdatedDate = DateTime.UtcNow.AddHours(4);
            feedback.IsDeleted = false;
            await _repository.CreateAsync(feedback);
            await _repository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var feedback = await _repository.SingleAsync(f => f.Id == id);
            if (feedback == null) throw new FeedbackNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/feedbacks", feedback.ImageUrl);
            _repository.Delete(feedback);
            await _repository.CommitAsync();
        }

        public async Task<List<Feedback>> GetAllAsync(Expression<Func<Feedback, bool>>? expression = null, params string[]? includes)
        {
            return await _repository.GetAllWhere(expression, includes).ToListAsync();
        }

        public Task<Feedback> GetByIdAsync(Expression<Func<Feedback, bool>>? expression = null, params string[]? includes)
        {
            return _repository.SingleAsync(expression, includes);   
        }

        public async Task SoftDelete(int id)
        {
            var feedback = await _repository.SingleAsync(f => f.Id == id);
            if (feedback == null) throw new FeedbackNullException("Entity cannot be null!");
            feedback.IsDeleted= !feedback.IsDeleted;
            await _repository.CommitAsync();
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            var existFeedback = await _repository.SingleAsync(f =>f.Id == feedback.Id);
            if (feedback == null) throw new FeedbackNullException("Entity cannot be null!");
            if (feedback.ImageFile != null)
            {
                if (feedback.ImageFile.ContentType != "image/jpeg" && feedback.ImageFile.ContentType != "image/png")
                {
                    throw new FeedbackImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
                }
                if (feedback.ImageFile.Length > 2097152)
                {
                    throw new FeedbackImageLengthException("ImageFile", "File size must be lower than 2mb!");
                }
                Helper.DeleteFile(_env.WebRootPath, "uploads/feedbacks", existFeedback.ImageUrl);
                existFeedback.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/feedbacks", feedback.ImageFile);
            }
            existFeedback.UpdatedDate = DateTime.UtcNow.AddHours(4);
            existFeedback.FullName = feedback.FullName;
            existFeedback.Comment = feedback.Comment;
            existFeedback.Date = feedback.Date;
            await _repository.CommitAsync();
        }
    }
}
