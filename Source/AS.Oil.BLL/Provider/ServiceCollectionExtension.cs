﻿using AS.Oil.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AS.Oil.DAL.Interfaces;
using AS.Oil.DAL.Repositories;

namespace AS.Oil.BLL.Provider
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterDbContext(this IServiceCollection services, string connectionString, string migrationProjectName)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly(migrationProjectName)));
        }

        public static void RegisterCollection(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}