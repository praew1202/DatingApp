using System;
using System.Collections.Generic;
using API.Data;
using API.Interfaces;
using API.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtentions
    {
             public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config){
             services.AddScoped<iTokenService,TokenService>();
                 //we add    
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
                
            });
            return services;
        }
        public static void ForEachT<T>(this List<T> list, Action<T,int> action){
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            for(int i = 0 ;i < list.Count ; i++){
                action(list[i],i);
            }
        }
    }
}