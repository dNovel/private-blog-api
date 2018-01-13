// <copyright file="BlogsController.cs" company="Dominik Steffen">
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.Logging;
    using NLog;
    using NLog.Extensions.Logging;

    [ApiVersion("0.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BlogsController : Controller
    {
        private readonly ILogger<BlogService> nLogger;

        private IBlogService blogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogsController"/> class.
        /// </summary>
        /// <param name="nLogger">The nLog instance</param>
        /// <param name="blogContext">The db context</param>
        public BlogsController(ILogger<BlogService> nLogger, BlogDbContext blogContext)
        {
            this.nLogger = nLogger;
            this.blogService = new BlogService(this.nLogger, blogContext);
        }

        [HttpGet]
        [Authorize("read:blog")]
        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.blogService.GetBlogs());
        }

        [HttpGet("{id}")]
        [Authorize("read:blog")]
        public async Task<IActionResult> Get(int id)
        {
            return this.Ok(await this.blogService.GetBlog(id));
        }

        [HttpPost]
        [Authorize("write:blog")]
        public async Task<IActionResult> Post([FromBody]Blog blogData)
        {
            return this.Ok(await this.blogService.CreateBlog(blogData));
        }

        [HttpPut("{id}")]
        [Authorize("write:blog")]
        public async Task<IActionResult> Put(int id, [FromBody]Blog blogData)
        {
            return this.Ok(await this.blogService.UpdateBlog(blogData));
        }

        [HttpDelete("{id}")]
        [Authorize("write:blog")]
        public async Task<IActionResult> Delete(int id)
        {
            return this.Ok(await this.blogService.DeleteBlog(id));
        }
    }
}
