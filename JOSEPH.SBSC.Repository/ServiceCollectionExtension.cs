using JOSEPH.SBSC.Repository.Repositories.AccountRepo;
using JOSEPH.SBSC.Repository.Repositories.CourseRepo;
using JOSEPH.SBSC.Repository.Repositories.ExamsRepo;
using JOSEPH.SBSC.Repository.Repositories.UserRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.Repository
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSBSCRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IExamsRepository, ExamsRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();

            return services;
        }
    }
}
