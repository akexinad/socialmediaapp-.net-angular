using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SocialMediaApp.API
{
    // The Program class is an entry point to the application.
    public class Program
    {
        // When we run our app, the Main method is the method that is invoked ...
        public static void Main(string[] args)
        {
            // ... which calls webhost builder function ...
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            // ... that calls CreateDefaultBuilder, and this uses kestrel to host the app ...
            WebHost.CreateDefaultBuilder(args)
                // ... then the StartUp methoos is called, taking us to the Startup class found in the Startup.cs file.
                .UseStartup<Startup>();
    }
}
