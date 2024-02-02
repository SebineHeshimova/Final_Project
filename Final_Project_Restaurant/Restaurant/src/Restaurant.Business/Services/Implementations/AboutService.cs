using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Restaurant.Business.CustomException.RestaurantException.AboutException;
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
			if (about == null) throw new AboutNullException("Entity cannot be null!");
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
				about.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/sliders", about.ImageFile);
			}
			about.CreatedDate = DateTime.UtcNow.AddHours(4);
			about.UpdatedDate = DateTime.UtcNow.AddHours(4);
			about.IsDeleted = false;
			await _repository.CreateAsync(about);
			await _repository.CommitAsync();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<About>> GetAllAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes)
		{
			throw new NotImplementedException();
		}

		public Task<About> GetByIdAsync(Expression<Func<About, bool>>? expression = null, params string[]? includes)
		{
			throw new NotImplementedException();
		}

		public Task SoftDelete(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(About about)
		{
			throw new NotImplementedException();
		}
	}
}
