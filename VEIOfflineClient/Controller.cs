using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceId;
using DeviceId.Windows.Wmi;

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
}
