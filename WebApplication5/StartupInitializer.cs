using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.DAL.Interfaces;
using WMS.DAL.Repositories;
using WMS.Domain.Entities;
using WMS.Service.Implementations;
using WMS.Service.Interfaces;

namespace WMS
{
    public static class StartupInitializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {              
        }

        public static void InitializeServices(this IServiceCollection services)
        {               
        }
    }
}
