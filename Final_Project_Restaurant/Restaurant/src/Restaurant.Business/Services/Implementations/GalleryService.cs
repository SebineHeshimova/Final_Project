using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.GalleryExceptions;
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
	public class GalleryService : IGalleryService
	{
		private readonly IGalleryRepository _repository;
		private readonly IWebHostEnvironment _env;
		public GalleryService(IGalleryRepository repository, IWebHostEnvironment env)
		{
			_repository = repository;
			_env = env;
		}

		public async Task CreateAsync(Gallery gallery)
		{
			if (gallery.ImageFile != null)
			{
				if (gallery.ImageFile.ContentType != "image/jpeg" && gallery.ImageFile.ContentType != "image/png")
				{
					throw new GalleryImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (gallery.ImageFile.Length > 2097152)
				{
					throw new GalleryImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				gallery.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/gallerys", gallery.ImageFile);
			}
			else
			{
				throw new GalleryNullException("ImageFile", "Entity cannot be null!");
			}
			gallery.CreatedDate = DateTime.UtcNow.AddHours(4);
			gallery.UpdatedDate = DateTime.UtcNow.AddHours(4);
			gallery.IsDeleted = false;
			await _repository.CreateAsync(gallery);
			await _repository.CommitAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var gallery = await _repository.SingleAsync(g=>g.Id == id);
			if (gallery == null) throw new GalleryNullException("Entity cannot be null!");
			Helper.DeleteFile(_env.WebRootPath, "uploads/gallerys", gallery.ImageUrl);
			_repository.Delete(gallery);
			await _repository.CommitAsync();
		}

		public async Task<List<Gallery>> GetAllAsync(Expression<Func<Gallery, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.GetAllWhere(expression, includes).ToListAsync();
		}

		public async Task<Gallery> GetByIdAsync(Expression<Func<Gallery, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.SingleAsync(expression, includes);
		}

		public async Task SoftDelete(int id)
		{
			var gallery = await _repository.SingleAsync(g => g.Id == id);
			if (gallery == null) throw new GalleryNullException("Entity cannot be null!");
			gallery.IsDeleted= !gallery.IsDeleted;
			await _repository.CommitAsync();
		}

		public async Task UpdateAsync(Gallery gallery)
		{
			var existGallery = await _repository.SingleAsync(g=>g.Id == gallery.Id);
			if (gallery == null) throw new GalleryNullException("Entity cannot be null!");
			if(gallery.ImageFile != null)
			{
				if (gallery.ImageFile.ContentType != "image/jpeg" && gallery.ImageFile.ContentType != "image/png")
				{
					throw new GalleryImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (gallery.ImageFile.Length > 2097152)
				{
					throw new GalleryImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				Helper.DeleteFile(_env.WebRootPath, "uploads/gallerys", existGallery.ImageUrl);
				existGallery.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/gallerys", gallery.ImageFile);
			}
			existGallery.UpdatedDate = DateTime.UtcNow.AddHours(4);
			existGallery.IsDeleted = false;
			await _repository.CommitAsync();
		}
	}
}
