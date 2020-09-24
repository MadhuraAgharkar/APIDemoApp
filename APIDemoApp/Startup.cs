using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemoApp.Business;
using APIDemoApp.Business.Interfaces;
using APIDemoApp.Controllers;
using APIDemoApp.Data;
using APIDemoApp.Data.Interfaces;
using APIDemoApp.Data.Models;
using APIDemoApp.Data.Repositories;
using APIDemoApp.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using static APIDemoApp.RemoveVersionFromParameter;

namespace APIDemoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            string connectionString = Configuration.GetSection("ConnectionString:Connection").Value;
            services.AddDbContext<LibraryDBContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, LibraryDBContext>();
            services.AddTransient<IBooksBusinessContract, BooksBusinessManager>();
            services.AddTransient<IBooksDataRepository, BooksDataRepository>();
            services.AddTransient<IAuthorDataRepository, AuthorDataRepository>();
            services.AddTransient<ICountryDataRepository, CountryDataRepository>();
            services.AddTransient<ILanguageDataRepository, LanguageDataRepository>();
            services.AddTransient<IGenreDataRepository, GenreDataRepository>();

            services.AddAuthentication("BasicAuthentication")
                     .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddSwaggerGen();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Demo v1",
                });
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "API Demo v2",
                });
                c.ResolveConflictingActions(a => a.First());
                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValue>();

            });

            services.AddApiVersioning(a=>{
                a.AssumeDefaultVersionWhenUnspecified = true;
                a.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            });
        }
    }
}
