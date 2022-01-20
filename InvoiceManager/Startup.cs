using InvoiceManager.AuthHandler;
using InvoiceManager.Data;
using InvoiceManager.Services.IServices;
using InvoiceManager.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InvoiceManager
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
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "v1",
                    Description = "Invoice Manager App V1",
                    Version = "V1.0.0"
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
                // Adds authentication to the generated json which is also picked up by swagger.
                options.AddSecurityDefinition(ApiKeyAuthConstants.DefaultScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = ApiKeyAuthConstants.ApiKeyHeaderName,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = ApiKeyAuthConstants.DefaultScheme
                });              

            });
            services.AddDbContext<InvoiceManagerAppDbContext>(options =>
                options.UseSqlite(optionsBuilder => optionsBuilder.MigrationsAssembly("InvoiceManager.Data")));
            services.AddControllers();
            services.AddTransient<IInvoiceService, InvoiceService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Manager App V1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
