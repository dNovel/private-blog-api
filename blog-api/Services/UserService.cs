// <copyright file="UserService.cs" company="Dominik Steffen">
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

    public class UserService
    {
        private readonly ILogger<BlogService> nLogger;
        private readonly BlogDbContext context;

        public UserService(ILogger<BlogService> nLogger, BlogDbContext blogContext)
        {
            this.nLogger = nLogger;
            this.context = blogContext;
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

        public async Task<User> GetUser(int id)
        {
            return await this.context.Users.SingleAsync(b => b.Id == id);
        }

        public async Task<User> UpdateUser(User userData)
        {
            throw new NotImplementedException();
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
    }
}
