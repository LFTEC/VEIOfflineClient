using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceId;
using DeviceId.Windows.Wmi;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace VEIOfflineClient
{
    public static class DeviceId
    {
        public static string Get()
        {
            var deviceId = new DeviceIdBuilder()
                .AddMachineName()
                .AddOsVersion()
                .OnWindows(windows => windows
                    .AddProcessorId()
                    .AddMotherboardSerialNumber()
                    .AddSystemDriveSerialNumber()
                )
                .ToString();
            return deviceId;
        }
    }

    public class ActivateInfo
    {
        public string ActivateCode { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
    }

    public class ConfigService
    {
        private readonly string _path;

        public ConfigService()
        {
            _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VEIOfflineClient/config.json");
        }


        public void UpdateActivateInfo(Action<ActivateInfo> updateAction)
        {

            var json = File.ReadAllText(_path);
            var jsonObject = JsonNode.Parse(json);
            var secretSection = jsonObject!["Secret"]?.AsObject();

            if(secretSection!= null)
            {
                var activateInfo = JsonSerializer.Deserialize<ActivateInfo>(secretSection.ToJsonString());
                updateAction(activateInfo!);

                jsonObject["Secret"] = new JsonObject { ["ActivateCode"] = activateInfo!.ActivateCode! };

                File.WriteAllText(_path, jsonObject.ToJsonString(new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
            }
 
        }
    }
}
