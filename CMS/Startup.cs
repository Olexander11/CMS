using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMS.Models.Feeds;
using CMS.Models.Logg;
using CMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // For creating db by code first in console PM>
            //  Add-Migration Initial
            //   Update-Database

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<FeedsContext>(options => options.UseSqlServer(connection));

            services.AddTransient<ITimeService, ReadFeedsService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ITimeService feedService)
        {
            DateTime now = DateTime.Now;
            string logFile = now.ToString("ddMMyy") + "logger.txt";
            loggerFactory.AddFile(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Logs"), logFile));
            var logger = loggerFactory.CreateLogger("FileLogger");


            feedService.TimeIntervalHours = 1;
            Task.Run(async () =>
            {
                await Task.Delay(1000 * 30);
                while (true)
                {

                    feedService.DoService();
                    await Task.Delay(feedService.TimeIntervalHours * 60 * 1000);

                }
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
