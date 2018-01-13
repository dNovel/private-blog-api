// <copyright file="IUserService.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlogAPI.Models;

    internal interface IUserService
    {
        Task<UserService> CreateUser(UserService userData);

        Task<UserService> GetUser(int id);

        Task<UserService> UpdateUser(UserService userData);

        Task<bool> SetUserInactive(int id);

        Task<bool> DeleteUser(int id);
    }
}
