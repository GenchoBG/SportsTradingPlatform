using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportsTrading.Data;
using SportsTrading.Data.Models;

namespace SportsTrading.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            string jsonPath = @"C:\Users\Kalin\Desktop\sport-events-backup.json";
            string json = File.ReadAllText(jsonPath);
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(json);
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<SportsTradingDbContext>().Database.Migrate();

                var db = serviceScope.ServiceProvider.GetService<SportsTradingDbContext>();

                Task
                    .Run(async () =>
                    {
                        if (!await db.Sports.AnyAsync())
                        {
                            // download data & fill up the database
                        }

                    })
                    .Wait();
            }

            return app;
        }
    }
}
