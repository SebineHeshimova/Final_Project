using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Restaurant.Business.CustomException.RestaurantException.VideoExceptions;
using Restaurant.Business.CustomException.RestaurantException.VideoExceptions;
using Restaurant.Business.CustomException.RestaurantException.VideoExceptions;
using Restaurant.Business.Extensions;
using Restaurant.Business.Services.Interfaces;
using Restaurant.Core.Entiity;
using Restaurant.Core.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Restaurant.Business.Services.Implementations
{
	public class VideoService : IVideoService
	{
		private readonly IVideoRepository _repository;
		private readonly IWebHostEnvironment _env;
		public VideoService(IVideoRepository repository, IWebHostEnvironment env)
		{
			_repository = repository;
			_env = env;
		}

		public async Task CreateAsync(Video video)
		{
			if (video.ImageFile != null)
			{
				if (video.ImageFile.ContentType != "image/jpeg" && video.ImageFile.ContentType != "image/png")
				{
					throw new VideoImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (video.ImageFile.Length > 2097152)
				{
					throw new VideoImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				video.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/videos", video.ImageFile);
			}
			else
			{
				throw new VideoNullException("ImageFile", "Entity cannot be null!");
			}
			video.CreatedDate = DateTime.UtcNow.AddHours(4);
			video.UpdatedDate = DateTime.UtcNow.AddHours(4);
			video.IsDeleted = false;
			await _repository.CreateAsync(video);
			await _repository.CommitAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var video = await _repository.SingleAsync(v =>v.Id == id);
			if (video == null) throw new VideoNullException("Entity cannot be null!");
			Helper.DeleteFile(_env.WebRootPath, "uploads/videos", video.ImageUrl);
			_repository.Delete(video);
			await _repository.CommitAsync();
		}

		public async Task<List<Video>> GetAllAsync(Expression<Func<Video, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.GetAllWhere(expression, includes).ToListAsync();
		}

		public async Task<Video> GetByIdAsync(Expression<Func<Video, bool>>? expression = null, params string[]? includes)
		{
			return await _repository.SingleAsync(expression, includes);
		}

		public async Task SoftDelete(int id)
		{
			var video = await _repository.SingleAsync(v =>v.Id == id);
			if (video == null) throw new VideoNullException("Entity cannot be null!");
			video.IsDeleted = !video.IsDeleted;
			await _repository.CommitAsync();
		}


		public async Task UpdateAsync(Video video)
		{
			var existVideo = await _repository.SingleAsync(g => g.Id == video.Id);
			if (video == null) throw new VideoNullException("Entity cannot be null!");
			if (video.ImageFile != null)
			{
				if (video.ImageFile.ContentType != "image/jpeg" && video.ImageFile.ContentType != "image/png")
				{
					throw new VideoImageContentTypeException("ImageFile", "File must be .png or .jpeg!");
				}
				if (video.ImageFile.Length > 2097152)
				{
					throw new VideoImageLengthException("ImageFile", "File size must be lower than 2mb!");
				}
				Helper.DeleteFile(_env.WebRootPath, "uploads/videos", existVideo.ImageUrl);
				existVideo.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/videos", video.ImageFile);
			}
			existVideo.UpdatedDate = DateTime.UtcNow.AddHours(4);
			existVideo.RedirectUrl = video.RedirectUrl;
			existVideo.IsDeleted = false;
			await _repository.CommitAsync();
		}
	}
}
