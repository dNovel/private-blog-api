// <copyright file="MediaController.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI.Controllers
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
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.Logging;
    using NLog;
    using NLog.Extensions.Logging;

    [ApiVersion("0.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MediaController : Controller
    {
        private readonly ILogger<BlogService> nLogger;

        private IMediaService mediaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaController"/> class.
        /// </summary>
        /// <param name="nLogger">The nLog instance</param>
        /// <param name="blogContext">The db context</param>
        public MediaController(ILogger<BlogService> nLogger, BlogDbContext blogContext)
        {
            this.nLogger = nLogger;
            this.mediaService = new MediaService(this.nLogger, blogContext);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.mediaService.GetMedia());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return this.Ok(await this.mediaService.GetMedia(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Media mediaData)
        {
            return this.Ok(await this.mediaService.CreateMedia(mediaData));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Media mediaData)
        {
            return this.Ok(await this.mediaService.UpdateMedia(mediaData));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return this.Ok(await this.mediaService.DeleteMedia(id));
        }
    }
}
