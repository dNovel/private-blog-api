// <copyright file="PostService.cs" company="Dominik Steffen">
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

    public class PostService : IPostService
    {
        private readonly ILogger<BlogService> nLogger;
        private readonly BlogDbContext context;

        public PostService(ILogger<BlogService> nLogger, BlogDbContext blogContext)
        {
            this.nLogger = nLogger;
            this.context = blogContext;
        }

        public async Task<Post> CreatePost(Post postData)
        {
            try
            {
                Post newPost = new Post
                {
                    Title = string.Empty,
                    Subtitle = string.Empty,
                    Teaser = string.Empty,
                    Content = string.Empty
                };

                this.nLogger.LogInformation("Created new post");

                await this.context.Posts.AddAsync(newPost);
                await this.context.SaveChangesAsync();

                return newPost;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return null;
            }
        }

        public async Task<bool> DeletePost(int id)
        {
            try
            {
                var postToRemove = this.context.Posts.Single(b => b.Id == id);
                this.context.Posts.Remove(postToRemove);
                await this.context.SaveChangesAsync();

                this.nLogger.LogInformation("Deleted post");

                return true;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return false;
            }
        }

        public async Task<Post> GetPost(int id)
        {
            return await this.context.Posts.SingleAsync(b => b.Id == id);
        }

        public async Task<List<Post>> GetPosts(int pageSize = 10, int page = 0)
        {
            var postsCount = this.context.Posts.Count<Post>();

            if (postsCount == 0)
            {
                return null;
            }
            else if (postsCount <= pageSize)
            {
                return await this.context.Posts.ToListAsync<Post>();
            }
            else
            {
                var posts = await this.context.Posts.ToListAsync<Post>();
                return posts.GetRange(page * pageSize, --pageSize);
            }
        }

        public async Task<Post> UpdatePost(Post postData)
        {
            try
            {
                var postToUpdate = await this.context.Posts.SingleAsync(b => b.Id == postData.Id);

                postToUpdate.Title = postData.Title;
                postToUpdate.Teaser = postData.Teaser;
                postToUpdate.Subtitle = postData.Subtitle;
                postToUpdate.Thumbnail = postData.Thumbnail;
                postToUpdate.Content = postData.Content;

                this.context.Posts.Update(postToUpdate);
                await this.context.SaveChangesAsync();
                return postToUpdate;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return null;
            }
        }
    }
}
