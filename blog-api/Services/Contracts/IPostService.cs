// <copyright file="IPostService.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlogAPI.Models;

    internal interface IPostService
    {
        Task<Post> CreatePost(Post postData);

        Task<Post> UpdatePost(Post postData);

        Task<List<Post>> GetPosts(int pageSize = 10, int page = 1);

        Task<Post> GetPost(int id);

        Task<bool> DeletePost(int id);
    }
}
