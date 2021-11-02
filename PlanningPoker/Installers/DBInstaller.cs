using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanningPoker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.WebApi.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(
                options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DBConnectionString")));
        }
    }
}
