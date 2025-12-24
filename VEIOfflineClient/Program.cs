using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;

namespace VEIOfflineClient
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var builder = Host.CreateApplicationBuilder(args);
            var service = builder.Services;
            var configuration = builder.Configuration;
            
            service.AddTransient<Form1>();

            using var host = builder.Build();
            using var serviceScope = host.Services.CreateScope();
            var mainForm = serviceScope.ServiceProvider.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }
    }
}