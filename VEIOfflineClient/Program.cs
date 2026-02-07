using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;
using Velopack;

namespace VEIOfflineClient
{
    internal static class Program
    {
        internal static Mutex? mutex = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            mutex = new Mutex(true, "Global\\5EE08AB0-7D3D-48BF-AC88-B7E03B98E651", out bool createdNew);
            if(!createdNew)
            {
                Process current = Process.GetCurrentProcess();

                foreach (var process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        var handle = process.MainWindowHandle;
                        if(handle != IntPtr.Zero)
                        {
                            if (NativeMethods.IsIconic(handle))
                            {
                                NativeMethods.ShowWindowAsync(handle, 9);
                            }
                            else
                                NativeMethods.ShowWindowAsync(handle, 5);
                            NativeMethods.SetForegroundWindow(handle);
                        }
                    }
                }
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            VelopackApp.Build().Run();
          
            ApplicationConfiguration.Initialize();
            Start startForm = new Start();
            startForm.Show();
            Application.DoEvents();

            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VEIOfflineClient");
            string configFile = Path.Combine(appDataPath, "config.json");
            if(!File.Exists(configFile))
            {
                if(!Path.Exists(appDataPath))
                {
                    Directory.CreateDirectory(appDataPath);
                }                
                var config = new { Secret = new ActivateInfo() };
                File.WriteAllText(configFile, JsonSerializer.Serialize(config));
            }

            var builder = Host.CreateApplicationBuilder(args);
            var service = builder.Services;
            var configuration = builder.Configuration;
            configuration.AddJsonFile(configFile, optional: true, reloadOnChange: true);

            var envSection = configuration.GetSection("Environment");
            service.Configure<EnvironmentInfo>(envSection);
            var environmentInfo = envSection.Get<EnvironmentInfo>();

            try
            {
                var version = UpdateProcess(environmentInfo!.UpdatePath);
                startForm.AddVersion(version);

                var secret = configuration.GetSection("Secret");
                service.Configure<ActivateInfo>(secret);
                var activateInfo = secret.Get<ActivateInfo>();

                SecurityConfigurationProvider securityConfigurationProvider = new SecurityConfigurationProvider();
                securityConfigurationProvider.SetDecryptedValue(DeviceId.Get(), activateInfo?.ActivateCode);
                ((IConfigurationBuilder)configuration).Add(new SecurityConfigurationSource(securityConfigurationProvider));
                service.Configure<SecurityInfo>(configuration.GetSection("Security"));
                service.AddSingleton(securityConfigurationProvider);
            }
            catch(HttpRequestException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Configuration error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            service.AddHttpClient<ICallApiService, CallApiService>((provider,client) =>
            {
                var environment = provider.GetRequiredService<IOptions<EnvironmentInfo>>().Value;
                var security = provider.GetRequiredService<IOptionsMonitor<SecurityInfo>>().CurrentValue;
                client.BaseAddress = new Uri(environment.BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Remove("Date");
                client.DefaultRequestHeaders.Add("X-Ca-Stage", environment.Stage);
                client.DefaultRequestHeaders.Add("X-Ca-Signature-Headers", "X-Ca-Stage");
                client.DefaultRequestHeaders.Add("x-ca-key", security.AppKey);
                client.DefaultRequestHeaders.Add("x-ca-signature-method", "HmacSHA256");
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    SslProtocols = System.Security.Authentication.SslProtocols.Tls12
                };
            });

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

            mainForm.Load += (s,e)=>
            {
                startForm.Close();
                startForm.Dispose();
            };

            host.Start();

            try
            {
                Application.Run(mainForm);
            }
            finally
            {
                host.StopAsync().GetAwaiter().GetResult();
                mutex?.ReleaseMutex();
                mutex?.Dispose();
            }

            GC.KeepAlive(mutex);

        }

        static async Task UpdateVersionHardAsync(string updatePath, SplashScreen1 splash)
        {
            var mgr = new UpdateManager(updatePath);
            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null) return;

            await mgr.DownloadUpdatesAsync(newVersion, progress =>
            {
                splash.BeginInvoke(() =>
                {
                    splash.ProcessCommand(SplashScreen1.SplashScreenCommand.SetProgress, progress);
                });
                

            });

            mgr.ApplyUpdatesAndRestart(newVersion);
        }

        static async Task UpdateVersionSoftAsync(string updatePath)
        {
            var mgr = new UpdateManager(updatePath);
            var newVersion = await mgr.CheckForUpdatesAsync();
            if (newVersion == null) return;

            await mgr.DownloadUpdatesAsync(newVersion);

        }

        static string? UpdateProcess(string updatePath)
        {
            var mgr = new UpdateManager(updatePath);
            var currentVersion = mgr.CurrentVersion;
            if (currentVersion == null) return null;

            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                httpClient.BaseAddress = new Uri(updatePath);
                var versionInfo = httpClient.GetFromJsonAsync<VersionInfo>($"version.json").GetAwaiter().GetResult();

                var minVersion = NuGet.Versioning.NuGetVersion.Parse(versionInfo!.minVersion);
                if (currentVersion < minVersion)
                {
                    var splash = new SplashScreen1();
                    splash.Load += async (s, e) =>
                    {
                        try
                        {
                            await UpdateVersionHardAsync(updatePath, splash);
                            splash.Close();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.ToString(), "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            splash.Close();
                        }
                    };

                    splash.ShowDialog();
                }
                else
                {
                    Task.Run(async () =>
                    {
                        await UpdateVersionSoftAsync(updatePath);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return currentVersion.ToFullString();
        }
    }

    internal record VersionInfo(string minVersion, string latestVersion);


    internal class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsIconic(IntPtr hWnd);
    }

}