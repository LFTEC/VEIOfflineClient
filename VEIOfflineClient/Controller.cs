using DeviceId;
using DeviceId.Windows.Wmi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

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

    public class EnvironmentInfo
    {
        public string Stage { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty; 
    }

    public record MasterData(string material, string materialDesc, bool deleted, string longText, string purLongText, string unit, string matType);

    public class StockMoveData
    {
        public string Vendor { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public List<StockMoveItem> Items { get; private set; } = new List<StockMoveItem>();
    }

    public record StockMoveItem(string material, string matType, string moveType, decimal quantity, string unit, string text);

    public interface ICallApiService
    {
        Task<List<MasterData>> GetMasterDataAsync();
        Task<string> SendStockInfoAsync(List<StockMoveItem> list);
    }

    public class CallApiService : ICallApiService
    {
        private readonly HttpClient _httpClient;
        private readonly EnvironmentInfo _apiSettings;
        private readonly SecurityInfo _security;
        private readonly ActivateInfo _activate;

        public CallApiService(HttpClient httpClient, IOptions<EnvironmentInfo> options, IOptionsMonitor<SecurityInfo> security, IOptions<ActivateInfo> activate)
        {
            _httpClient = httpClient;
            _apiSettings = options.Value;
            _security = security.CurrentValue;
            _activate = activate.Value;
        }

        private record RequestData(string type, string timestamp, object data);

        private async Task<string> PostAsync(RequestData requestData)
        {
            string message = System.Text.Json.JsonSerializer.Serialize(requestData, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            var content = new StringContent(message, System.Text.Encoding.UTF8, "application/json");

            string path = _apiSettings.Path;
            string method = "POST";
            string accept = "application/json";
            string contentMd5 = "";// Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(message)));
            string contentType = "application/json; charset=utf-8";
            string header = $"X-Ca-Stage:{_apiSettings.Stage}";

            StringBuilder sb = new StringBuilder();
            sb.AppendJoin("\n",
                method,
                accept,
                contentMd5,
                contentType,
                "", // Date is empty
                header,
                path
            );

            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_security.AppSecret));
            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString())));
            var request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Headers.Add("X-Ca-Signature", signature);
            request.Headers.Remove("Date");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var ret = await response.Content.ReadAsStringAsync();
                var resultNode = JsonNode.Parse(ret);
                if (resultNode != null)
                {
                    var state = resultNode["state"]?.GetValue<string>();
                    if (state == "E")
                        throw new Exception($"Error response from API. {resultNode["msgText"]?.GetValue<string>() ?? "Unknown error"}");

                    return resultNode["data"]?.ToJsonString()!;
                }
                else
                {
                    throw new Exception("Invalid response from API");
                }
            }
            else
            {
                throw new Exception($"API request failed with status code: {response.StatusCode}, message: {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<List<MasterData>> GetMasterDataAsync()
        {
            var data = new RequestData("API_VEI_GET_STOCK", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new { vendor = _security.Vendor, deviceId = _activate.DeviceId });
            var result = await PostAsync(data);
            var masterData = JsonSerializer.Deserialize<List<MasterData>>(result!, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            }) ?? new List<MasterData>();

            return masterData;
        }

        public async Task<string> SendStockInfoAsync(List<StockMoveItem> list)
        {   
            var data = new StockMoveData
            {
                Vendor = _security.Vendor,
                DeviceId = _activate.DeviceId
            };

            data.Items.AddRange(list);
            var body = new RequestData(type: "API_VEI_STOCK_MOVE", timestamp: DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), data: data);
            var result = await PostAsync(body);
            return "";
        }
    }
}
