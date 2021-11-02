using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.WebApi.Installers
{
    interface IInstaller
    {
        void InstallerService(IServiceCollection services, IConfiguration configuration);
    }
}
