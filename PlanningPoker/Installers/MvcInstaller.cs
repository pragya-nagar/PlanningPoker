using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanningPoker.Repository.Class;
using PlanningPoker.Repository.Interface;
using PlanningPoker.Action.Class;
using PlanningPoker.Action.Interface;
using PlanningPoker.Repository;

namespace PlanningPoker.WebApi.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IResetPasswordService, ResetPasswordService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IUserStoryservice, UserStoryService>();
            services.AddTransient<IInviteUserService, InviteUserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserSessionService, UserSessionService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();

            //Repository
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IResetPasswordRepository, ResetPasswordRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IUserStoryRepository, UserStoryRepository>();
            services.AddTransient<IInviteUserRepository, InviteUserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserSessionRepository, UserSessionRepository>();
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IEmailSenderRepository, EmailSenderRepository>();
        }
    }
}
