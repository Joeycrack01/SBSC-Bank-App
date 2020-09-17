using JOSEPH.SBSC.ApplicationService.Services.CourseServices;
using JOSEPH.SBSC.ApplicationService.Services.ExamServices;
using JOSEPH.SBSC.ApplicationService.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JOSEPH.SBSC.ApplicationService
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSBSCApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<IExamAppService, ExamAppService>();
            services.AddTransient<ICourseAppService, CourseAppService>();

            return services;
        }
    }
}
