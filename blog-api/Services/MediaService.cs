// <copyright file="MediaService.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlogAPI.Database;
    using BlogAPI.Models;
    using BlogAPI.Services;
    using BlogAPI.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class MediaService : IMediaService
    {
        private readonly ILogger<BlogService> nLogger;
        private readonly BlogDbContext context;

        public MediaService(ILogger<BlogService> nLogger, BlogDbContext blogContext)
        {
            this.nLogger = nLogger;
            this.context = blogContext;
        }

        public async Task<Media> CreateMedia(Media mediaData)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Media>> GetMedia(int pageSize, int page)
        {
            var media = await this.context.Media.ToListAsync<Media>();

            if (media.Count == 0)
            {
                return null;
            }
            else if (media.Count <= pageSize)
            {
                return media.GetRange(0, media.Count - 1);
            }
            else if (page <= 1)
            {
                return media.GetRange(0, pageSize--);
            }
            else
            {
                return media.GetRange(((page - 1) * pageSize) - 1, pageSize);
            }
        }

        public async Task<Media> GetMedia(int id)
        {
            return await this.context.Media.SingleAsync(b => b.Id == id);
        }

        public async Task<Media> UpdateMedia(Media mediaData)
        {
            try
            {
                var mediaToUpdate = await this.context.Media.SingleAsync(b => b.Id == mediaData.Id);

                mediaToUpdate.Name = mediaData.Name;
                mediaToUpdate.FileName = mediaData.FileName;
                mediaToUpdate.FileType = mediaData.FileType;
                mediaToUpdate.Stream = mediaData.Stream;

                this.context.Media.Update(mediaToUpdate);
                await this.context.SaveChangesAsync();
                return mediaToUpdate;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return null;
            }
        }

        public async Task<bool> DeleteMedia(int id)
        {
            try
            {
                var mediaToRemove = this.context.Media.Single(b => b.Id == id);
                this.context.Media.Remove(mediaToRemove);
                await this.context.SaveChangesAsync();

                this.nLogger.LogInformation("Deleted media");

                return true;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return false;
            }
        }
    }
}
