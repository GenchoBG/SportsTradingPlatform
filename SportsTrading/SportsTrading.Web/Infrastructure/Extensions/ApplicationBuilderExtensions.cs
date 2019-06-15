using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsTrading.Data;

namespace SportsTrading.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
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
