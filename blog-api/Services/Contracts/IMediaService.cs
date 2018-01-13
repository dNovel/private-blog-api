// <copyright file="IMediaService.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlogAPI.Models;

    internal interface IMediaService
    {
        Task<Media> CreateMedia(Media mediaData);

        Task<List<Media>> GetMedia(int pageSize = 10, int page = 0);

        Task<Media> GetMedia(int id);

        Task<Media> UpdateMedia(Media mediaData);

        Task<bool> DeleteMedia(int id);
    }
}
