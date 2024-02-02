using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.SliderException;
using Restaurant.Business.CustomException.RestaurantException.WrapperExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Services.Implementations
{
	public class WrapperService : IWrapperService
	{
		private readonly IWrapperRepository _repository;
		private readonly IWebHostEnvironment _env;
		public WrapperService(IWebHostEnvironment env, IWrapperRepository repository)
		{

			_env = env;
			_repository = repository;
		}

		public async Task CreateAsync(Wrapper wrapper)
		{
			if (wrapper == null) throw new WrapperNullException("Entity cannot be null!");
			if (wrapper.ImageFile != null)
			{
				if (wrapper.ImageFile.ContentType != "image/jpeg" && wrapper.ImageFile.ContentType != "image/png")
				{
					throw new WrapperImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (wrapper.ImageFile.Length > 2097152)
				{
					throw new WrapperImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				wrapper.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/wrappers", wrapper.ImageFile);
			}
			wrapper.CreatedDate = DateTime.UtcNow.AddHours(4);
			wrapper.UpdatedDate = DateTime.UtcNow.AddHours(4);
			wrapper.IsDeleted = false;
			await _repository.CreateAsync(wrapper);
			await _repository.CommitAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var wrapper = await _repository.SingleAsync(w => w.Id == id);
			if (wrapper == null) throw new WrapperNullException("Entity cannot be null!");
			Helper.DeleteFile(_env.WebRootPath, "uploads/wrappers", wrapper.ImageUrl);
			_repository.Delete(wrapper);
			await _repository.CommitAsync();
		}

		public async Task<List<Wrapper>> GetAllAsync(Expression<Func<Wrapper, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.GetAllWhere(expression, includes).ToListAsync();
		}

		public async Task<Wrapper> GetByIdAsync(Expression<Func<Wrapper, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.SingleAsync(expression, includes);
		}

		public async Task SoftDelete(int id)
		{
			var wrapper=await _repository.SingleAsync(x=>x.Id==id);
			if (wrapper == null) throw new WrapperNullException("Entity cannot be null!");
			wrapper.IsDeleted = !wrapper.IsDeleted;
			await _repository.CommitAsync();
		}

		public async Task UpdateAsync(Wrapper wrapper)
		{
			var existWrapper = await _repository.SingleAsync(s => s.Id == wrapper.Id);
			if (existWrapper == null) throw new WrapperNullException("Entity cannot be null!");
			if (wrapper.ImageFile != null)
			{
				if (wrapper.ImageFile.ContentType != "image/jpeg" && wrapper.ImageFile.ContentType != "image/png")
				{
					throw new WrapperImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (wrapper.ImageFile.Length > 2097152)
				{
					throw new WrapperImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				Helper.DeleteFile(_env.WebRootPath, "uploads/wrappers", existWrapper.ImageUrl);
				existWrapper.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/wrappers", wrapper.ImageFile);
			}
			existWrapper.UpdatedDate = DateTime.UtcNow.AddHours(4);
			existWrapper.Title = wrapper.Title;
			existWrapper.Description = wrapper.Description;
			existWrapper.SubDescription= wrapper.SubDescription;
			existWrapper.RedirectText = wrapper.RedirectText;
			existWrapper.RedirectUrl = wrapper.RedirectUrl;
			await _repository.CommitAsync();
		}
	}
}
