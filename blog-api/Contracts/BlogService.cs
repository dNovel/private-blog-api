// <copyright file="BlogService.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlogAPI.Database;
    using BlogAPI.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class BlogService : IBlogService
    {
        private readonly ILogger<BlogService> nLogger;
        private readonly BlogDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService"/> class.
        /// </summary>
        /// <param name="nLogger">The nLog instance</param>
        /// <param name="blogContext">The blog context</param>
        public BlogService(ILogger<BlogService> nLogger, BlogDbContext blogContext)
        {
            this.nLogger = nLogger;
            this.context = blogContext;
        }

        public async Task<Blog> CreateBlog(Blog blogData)
        {
            try
            {
                List<User> firstOwner = new List<User>();

                // TODO: Hang in my user at least
                User admin = new User
                {
                    FirstName = "Dominik",
                    LastName = "Steffen",
                    DisplayName = "dominik.steffen",
                    Email = "dominik.steffen@gmail.com",
                    Password = "test"
                };

                firstOwner.Add(admin);

                Blog newBlog = blogData;

                List<User> admins = new List<User>
                {
                    admin
                };
                newBlog.Owner = admins;

                this.context.Users.Add(admin);
                this.context.Blogs.Add(newBlog);
                await this.context.SaveChangesAsync();

                this.nLogger.LogInformation("Created new admin user");
                this.nLogger.LogInformation("Created new blog");
                return newBlog;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return null;
            }
        }

        public async Task<Media> CreateMedia(Media mediaData)
        {
            throw new NotImplementedException();
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

        public async Task<User> CreateUser(User userData)
        {
            try
            {
                User newUser = new User
                {
                    DisplayName = userData.DisplayName,
                    Email = userData.Email,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Password = userData.Password // TODO hash this password
                };

                await this.context.Users.AddAsync(newUser);
                await this.context.SaveChangesAsync();

                this.nLogger.LogInformation("Created new user");

                return newUser;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return null;
            }
        }

        public async Task<bool> DeleteBlog(int id)
        {
            try
            {
                var blogToRemove = this.context.Blogs.Single(b => b.Id == id);
                this.context.Blogs.Remove(blogToRemove);
                await this.context.SaveChangesAsync();

                this.nLogger.LogInformation("Deleted blog");

                return true;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return false;
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

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var userToRemove = this.context.Users.Single(b => b.Id == id);
                this.context.Users.Remove(userToRemove);
                await this.context.SaveChangesAsync();

                this.nLogger.LogInformation("Deleted user");

                return true;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return false;
            }
        }

        public async Task<Blog> GetBlog(int id)
        {
            return await this.context.Blogs.SingleAsync(b => b.Id == id);
        }

        public async Task<List<Blog>> GetBlogs()
        {
            return await this.context.Blogs.ToListAsync();
        }

        public async Task<Media> GetMedia(int id)
        {
            return await this.context.Media.SingleAsync(b => b.Id == id);
        }

        public async Task<Post> GetPost(int id)
        {
            return await this.context.Posts.SingleAsync(b => b.Id == id);
        }

        public async Task<List<Post>> GetPosts(int pageSize = 10, int page = 1)
        {
            var posts = await this.context.Posts.ToListAsync<Post>();

            if (posts.Count == 0)
            {
                return null;
            }
            else if (posts.Count <= pageSize)
            {
                return posts.GetRange(0, posts.Count - 1);
            }
            else if (page <= 1)
            {
                return posts.GetRange(0, pageSize--);
            }
            else
            {
                return posts.GetRange(((page - 1) * pageSize) - 1, pageSize);
            }
        }

        public async Task<User> GetUser(int id)
        {
            return await this.context.Users.SingleAsync(b => b.Id == id);
        }

        public async Task<bool> SetBlogInactive(int id)
        {
            return false;
        }

        public async Task<bool> SetUserInactive(int id)
        {
            return false;
        }

        public async Task<Blog> UpdateBlog(Blog blogData)
        {
            try
            {
                var blogToUpdate = await this.context.Blogs.SingleAsync(b => b.Id == blogData.Id);

                blogToUpdate.Name = blogData.Name;
                blogToUpdate.Url = blogData.Url;

                this.context.Blogs.Update(blogToUpdate);
                await this.context.SaveChangesAsync();
                return blogToUpdate;
            }
            catch (Exception e)
            {
                this.nLogger.LogError("Exception thrown: " + e.Message);
                return null;
            }
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

        public async Task<User> UpdateUser(User userData)
        {
            throw new NotImplementedException();
        }
    }
}
