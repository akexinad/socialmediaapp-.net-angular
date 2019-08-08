using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Helpers;

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
            // NOTE: The controller will only be fed the IAuth Repository so the code in the controller will never have to change.
            services.AddScoped<IAuthRepository, AuthRepository>();
            // Authentication Middleware for the Authorize attribute at the top of the AuthController.
            // You will then need to call app.UseAuthentication() in the configure method below.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // NOTE: The ordering of the services is important here.
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
                // THE GLOBAL EXCEPTION HANDLER
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            // The error that we get from the server is still too abstract.
                            // We create an extension method in the helpers folder in order
                            // to be provided with a more helpful error message.
                            context.Response.AddApplicationError(error.Error.Message);
                            
                            await context.Response.WriteAsync(error.Error.Message);

                            // Now with this helper, we can access these newly added headers on the client side
                            // and provide more detailed errors.
                        }
                    });
                });
                
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
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
