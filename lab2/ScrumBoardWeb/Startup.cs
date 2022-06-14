using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using ScrumBoard.Repository;
using ScrumBoardWeb.Infrastructure.Database.DBContext;
using ScrumBoardWeb.Application.DTO.Mapper;
using ScrumBoardWeb.Application.Service;
using ScrumBoardWeb.Infrastructure.Mapper;
using ScrumBoardWeb.Infrastructure.Repository;
using ScrumBoardWeb.Infrastructure.Service;
using ScrumBoardWeb.Infrastructure.Database;

namespace ScrumBoardWeb
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
            services
                .AddDbContext<ScrumBoardDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddScoped<IDatabaseEntityHydrator, DatabaseEntityHydrator>()
                .AddScoped<CardDtoMapperInterface, CardDtoMapper>()
                .AddScoped<IScrumBoardRepository, ScrumBoardRepository>()
                .AddScoped<IScrumBoardService, ScrumBoardService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScrumBoardWeb", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScrumBoardWeb v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
