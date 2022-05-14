using HR_Management_System.Data;
using HR_Management_System.Models.Interfaces;
using HR_Management_System.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Timers;
using System;
using Microsoft.OpenApi.Models;

namespace HR_Management_System
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HR_DbContext>(options =>
            {
                // Our DATABASE_URL from js days
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddControllers();

            services.AddTransient<ITicket, TicketService>();
            services.AddTransient<IDepartment, DepartmentService>();
            services.AddTransient<IEmployee, EmployeeService>();
            services.AddTransient<IAttendance, AttendanceService>();
            services.AddTransient<ISalarySlip, SalarySlipService>();


            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // Swagger added.
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title = "HR Management System",
                    Version = "V1",
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Also for swagger.
            app.UseSwagger(opt =>
            {
                opt.RouteTemplate = "/api/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/api/V1/swagger.json", "HR Management System");
                opt.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
