using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.AboutException;
using Restaurant.Business.CustomException.RestaurantException.BannerExceptions;
using Restaurant.Business.CustomException.RestaurantException.SliderException;
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
	public class AboutService : IAboutService
	{
		private readonly IAboutRepository _repository;
		private readonly IWebHostEnvironment _env;
		public AboutService(IAboutRepository repository, IWebHostEnvironment env)
		{
			_repository = repository;
			_env = env;
		}

		public async Task CreateAsync(About about)
		{
			if (about.ImageFile != null)
			{
				if (about.ImageFile.ContentType != "image/jpeg" && about.ImageFile.ContentType != "image/png")
				{
					throw new AboutImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (about.ImageFile.Length > 2097152)
				{
					throw new AboutImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				about.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/abouts", about.ImageFile);
			}
            else
            {
                throw new AboutNullException("ImageFile", "Entity cannot be null!");
            }
			if (about.SignatureImageFile != null)
			{
				if (about.SignatureImageFile.ContentType != "image/jpeg" && about.SignatureImageFile.ContentType != "image/png")
				{
					throw new AboutSignatureImageContentTypeException("SignatureImageFile", "File must be .png or .jpeg!");
				}
				if (about.SignatureImageFile.Length > 2097152)
				{
					throw new AboutSignatureImageLengthException("SignatureImageFile", "File size must be lower than 2mb!");
				}
				about.SignatureImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/abouts", about.SignatureImageFile);
			}
			else
			{
				throw new AboutSignatureImageNullException("SignatureImageFile", "Entity cannot be null!");
			}
			about.CreatedDate = DateTime.UtcNow.AddHours(4);
			about.UpdatedDate = DateTime.UtcNow.AddHours(4);
			about.IsDeleted = false;
			await _repository.CreateAsync(about);
			await _repository.CommitAsync();
		}

        public async Task DeleteAsync(int id)
        {
            var about = await _repository.SingleAsync(x => x.Id == id);
            if (about == null) throw new SliderNullException("Entity cannot be null!");
            Helper.DeleteFile(_env.WebRootPath, "uploads/abouts", about.ImageUrl);
			Helper.DeleteFile(_env.WebRootPath, "uploads/abouts", about.SignatureImageUrl);
			_repository.Delete(about);
            await _repository.CommitAsync();
        }

        public async Task<List<About>> GetAllAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.GetAllWhere(expression, includes).ToListAsync();
		}

		public async Task<About> GetByIdAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.SingleAsync(expression, includes);
		}

        public async Task SoftDelete(int id)
        {
            var about=await _repository.SingleAsync(x => x.Id==id);
			if (about == null) throw new AboutNullException("Entity cannot be null!");
			about.IsDeleted = !about.IsDeleted;
			await _repository.CommitAsync();
        }

        public async Task UpdateAsync(About about)
		{
			var existAbout = await _repository.SingleAsync(s => s.Id == about.Id);
			if (existAbout == null) throw new AboutNullException("Entity cannot be null!");
			if (about.ImageFile != null)
			{
				if (about.ImageFile.ContentType != "image/jpeg" && about.ImageFile.ContentType != "image/png")
				{
					throw new AboutImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (about.ImageFile.Length > 2097152)
				{
					throw new AboutImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				Helper.DeleteFile(_env.WebRootPath, "uploads/abouts", existAbout.ImageUrl);
				existAbout.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/abouts", about.ImageFile);
			}
			if (about.SignatureImageFile != null)
			{
				if (about.SignatureImageFile.ContentType != "image/jpeg" && about.SignatureImageFile.ContentType != "image/png")
				{
					throw new AboutSignatureImageContentTypeException("SignatureImageFile", "File must be .png or .jpeg!");
				}
				if (about.SignatureImageFile.Length > 2097152)
				{
					throw new AboutSignatureImageLengthException("SignatureImageFile", "File size must be lower than 2mb!");
				}
				Helper.DeleteFile(_env.WebRootPath, "uploads/abouts", existAbout.SignatureImageUrl);
				existAbout.SignatureImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/abouts", about.SignatureImageFile);
			}
			existAbout.UpdatedDate = DateTime.UtcNow.AddHours(4);
			existAbout.Title = about.Title;
			existAbout.Description = about.Description;
			existAbout.RedirectUrl = about.RedirectUrl;
			await _repository.CommitAsync();
		}
	}
}
