// <copyright file="IBlogService.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Contracts
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

        Task<Post> CreatePost(Post postData);

        Task<Post> UpdatePost(Post postData);

        Task<List<Post>> GetPosts(int pageSize = 10, int page = 1);

        Task<Post> GetPost(int id);

        Task<bool> DeletePost(int id);

        Task<User> CreateUser(User userData);

        Task<User> GetUser(int id);

        Task<User> UpdateUser(User userData);

        Task<bool> SetUserInactive(int id);

        Task<bool> DeleteUser(int id);

        Task<Media> CreateMedia(Media mediaData);

        Task<Media> GetMedia(int id);

        Task<Media> UpdateMedia(Media mediaData);

        Task<bool> DeleteMedia(int id);
    }
}
