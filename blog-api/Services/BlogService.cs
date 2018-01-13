// <copyright file="BlogService.cs" company="Dominik Steffen">
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

        public async Task<Blog> GetBlog(int id)
        {
            return await this.context.Blogs.SingleAsync(b => b.Id == id);
        }

        public async Task<List<Blog>> GetBlogs()
        {
            return await this.context.Blogs.ToListAsync();
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
    }
}
