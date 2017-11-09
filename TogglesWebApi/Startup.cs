using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Toggles.DataAccess;
using Microsoft.EntityFrameworkCore;
using Toggles.Repositories;
using Toggles.Repositories.Contracts;
using Toggles.BusinessRules;
using Toggles.BusinessRules.Contracts;

namespace TogglesWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = this.Configuration.GetConnectionString("TogglesConnectionString");
            services.AddDbContext<TogglesDbContext>(options => options.UseSqlServer(connectionString));
            services.AddMvc();

            services.AddScoped<IClientApplicationToggleValuesRepository, ClientApplicationToggleValuesRepository>();
            services.AddScoped<ITogglesReadRepository, TogglesReadRepository>();
            services.AddScoped<ITogglesRepository, TogglesRepository>();
            services.AddScoped<IToggleValuesReadRepository, ToggleValuesReadRepository>();
            services.AddScoped<ITogglesUnitOfWork, TogglesUnitOfWork>();
            services.AddTransient<ITogglesLoader, TogglesLoader>();
            services.AddTransient<IToggleValuesLoader, ToggleValuesLoader>();
            services.AddTransient<ICreateToggleCommand, CreateToggleCommand>();
            services.AddTransient<IUpdateToggleCommand, UpdateToggleCommand>();
            services.AddTransient<IDeleteToggleCommand, DeleteToggleCommand>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
