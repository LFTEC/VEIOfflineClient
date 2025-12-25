using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using System.IO;
using System.Text.Json;

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
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VEIOfflineClient");
            string configFile = Path.Combine(appDataPath, "config.json");
            if(!Path.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
                var config = new { Secret = new ActivateInfo() };
                File.WriteAllText(configFile, JsonSerializer.Serialize(config));
            }

            var builder = Host.CreateApplicationBuilder(args);
            var service = builder.Services;
            var configuration = builder.Configuration;
            configuration.AddJsonFile(configFile, optional: true, reloadOnChange: true);

            var secret = configuration.GetSection("Secret");
            service.Configure<ActivateInfo>(secret);
            var activateInfo = secret.Get<ActivateInfo>();

            SecurityConfigurationProvider securityConfigurationProvider = new SecurityConfigurationProvider();
            securityConfigurationProvider.SetDecryptedValue(DeviceId.Get(), activateInfo?.ActivateCode);
            ((IConfigurationBuilder)configuration).Add(new SecurityConfigurationSource(securityConfigurationProvider));



            service.AddSingleton<ConfigService>();
            service.AddTransient<Form1>();
            service.AddTransient<DXItem1>();
            service.PostConfigure<ActivateInfo>(info =>
            {
                info.DeviceId = DeviceId.Get(); ;
            });

            using var host = builder.Build();
            using var serviceScope = host.Services.CreateScope();
            var mainForm = serviceScope.ServiceProvider.GetRequiredService<Form1>();

            

            host.Start();
            Application.Run(mainForm);

            host.StopAsync().GetAwaiter().GetResult();
        }
    }
}