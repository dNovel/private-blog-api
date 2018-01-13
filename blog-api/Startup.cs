// <copyright file="Startup.cs" company="Dominik Steffen">
// Software is not for free use. If you want to use it please contact me at dominik.steffen@gmail.com
// </copyright>

namespace BlogAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BlogAPI.Authorization;
    using BlogAPI.Database;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using NLog.Extensions.Logging;
    using NLog.Web;

    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;

            hostingEnvironment.ConfigureNLog("NLog.conf");
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public NLog.Logger NLogger { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure environments
            if (this.HostingEnvironment.IsDevelopment())
            {
                services.AddDbContext<BlogDbContext>(opt =>
                {
                    opt.UseSqlServer(this.Configuration.GetConnectionString("DbConnection"));
                });
            }
            else if (this.HostingEnvironment.IsStaging())
            {
                services.AddDbContext<BlogDbContext>(opt =>
                {
                    opt.UseSqlServer(this.Configuration.GetConnectionString("DbConnection"));
                });
            }
            else if (this.HostingEnvironment.IsProduction())
            {
                services.AddDbContext<BlogDbContext>(opt =>
                {
                    opt.UseSqlServer(this.Configuration.GetConnectionString("DbConnection"));
                });
            }

            // Configure authentication
            string domain = $"{this.Configuration["Auth0:Protocol"]}{this.Configuration["Auth0:Domain"]}";
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.Authority = domain;
                opt.Audience = this.Configuration["Auth0:ApiIdentifier"];
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("read:blog", pol => pol.Requirements.Add(new HasScopeRequirement("read:blog", domain)));
                opt.AddPolicy("write:blog", pol => pol.Requirements.Add(new HasScopeRequirement("write:blog", domain)));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddApiVersioning();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // NLog config
            loggerFactory.AddNLog();
            app.AddNLogWeb();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
