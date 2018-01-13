// <copyright file="IBlogService.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlogAPI.Models;
    using Microsoft.AspNetCore.Mvc;

    internal interface IBlogService
    {
        Task<Blog> CreateBlog(Blog blogData);

        Task<Blog> UpdateBlog(Blog blogData);

        Task<Blog> GetBlog(int id);

        Task<List<Blog>> GetBlogs();

        Task<bool> DeleteBlog(int id);

        Task<bool> SetBlogInactive(int id);
    }
}
