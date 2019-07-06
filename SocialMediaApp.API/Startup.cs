﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SocialMediaApp.API.Data;

namespace SocialMediaApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // This property is where you can access the configuration key/value pairs in the appsettings.json file.
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // We add a DbContext of type DataContext, and pass in options to use Sqlite.
            // To install Sqlite, we use the nuget package manager.
            // The value of the connection string is declared in appsettings.json
            services.AddDbContext<DataContext>(db => db.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Similar to node, this is a conditional that checks the environment.
            if (env.IsDevelopment())
            {   
                // If an expection method is hit in the controller during development mode, it will throw out a nice developer exception page.
                // If you change to development in your launchsettings json file. You wont get an eror page.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

                // This is a security enhancement which stops all communications from going over HTTP, and makes sure they go over HTTPS
                // These two lines below will be temporarily commented out. Security is not really the concern for this app and we do not want it to redirect to HTTPS.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();

            // This method gives us the ability to route to different actions.
            // Mvc is a form of middleware that hooks up your backend end point to the client requests.
            // It routes our requests to the right controller.
            app.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();
        }
    }
}
