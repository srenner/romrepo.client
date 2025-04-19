using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using romrepo.lib;
using romrepo.lib.DataAccess;
using romrepo.lib.Services.Interfaces;
using romrepo.lib.Services;
using RomRepo.service.Services;


namespace romrepo.win
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<HostOptions>(options =>
            {
                options.ShutdownTimeout = TimeSpan.FromSeconds(30);
            });

            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "RomRepo Service";
            });
            
            builder.Services.AddHostedService<ScopedBackgroundService>();
            builder.Services.AddScoped<IScopedProcessingService, Worker>();

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddRazorPages();
            builder.Services.AddDbContext<RomRepoContext>();
            builder.Services.AddScoped<IClientRepo, ClientRepo>();
            builder.Services.AddScoped<IAppService, AppService>();
            builder.Services.AddScoped<IRomService, RomService>();
            builder.Services.AddScoped<ICoreService, CoreService>();
            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddMemoryCache();
            var host = builder.Build();

            host.UseSwagger();
            host.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RomRepo API v1");
                c.RoutePrefix = "swagger";
            });

            host.UseDefaultFiles();
            host.UseStaticFiles();
            host.MapControllers();

            Task webTask = host.RunAsync();
            webTask.Wait();
        }
    }
}