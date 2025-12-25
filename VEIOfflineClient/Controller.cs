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
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;

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

    public class SecurityInfo
    {
        public string Vendor {  get; set; } = string.Empty;
        public string AppKey {  get; set; } = string.Empty;
        public string AppSecret { get; set;  } = string.Empty;
    }

    public class SecurityConfigurationProvider: ConfigurationProvider
    {
        private string key = "Security";
        public void SetDecryptedValue(string deviceId, string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Data[$"{key}:Vendor"] = "";
                Data[$"{key}:AppKey"] = "";
                Data[$"{key}:AppSecret"] = "";
            }
            else
            {
                var engine = new CbcBlockCipher(new AesEngine());
                var cipher = new PaddedBufferedBlockCipher(engine, new Pkcs7Padding());

                var encryptedData = Convert.FromBase64String(value);

                var keyString = Encoding.UTF8.GetBytes(deviceId);
                var keyCipher = SHA256.HashData(keyString);

                var IV = new byte[16];
                Array.Copy(encryptedData, IV, IV.Length);

                var paramWithIV = new ParametersWithIV(new KeyParameter(keyCipher), IV);
                cipher.Init(false, paramWithIV);

                var cipherText = new byte[encryptedData.Length - 16];
                Array.Copy(encryptedData, 16, cipherText, 0, cipherText.Length);
                var output = new byte[cipher.GetOutputSize(cipherText.Length)];
                int length = cipher.ProcessBytes(cipherText, 0, cipherText.Length, output, 0);
                int finalLength = cipher.DoFinal(output, length);
                byte[] result = new byte[length + finalLength];
                Array.Copy(output, 0, result, 0, result.Length);

                var resultText = Encoding.UTF8.GetString(result);

                var splitData = resultText.Split(':');
                Data[$"{key}:Vendor"] = splitData[0];
                Data[$"{key}:AppKey"] = splitData[1];
                Data[$"{key}:AppSecret"] = splitData[2];
            }

            OnReload();
            
        }
    }

    public class SecurityConfigurationSource : IConfigurationSource
    {
        private readonly SecurityConfigurationProvider _provider;

        public SecurityConfigurationSource(SecurityConfigurationProvider provider)
        {
            _provider = provider;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return _provider;
        }
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
