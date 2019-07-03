using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace PCCG_Tester
{
    public static class DMChecker
    {
        
        /*foreach (var device in devices)
        {
            Console.WriteLine("Device ID: {0}, PNP Device ID: {1}, Description: {2}, Status: {3}",
                device.DeviceID, device.PnpDeviceID, device.Description, device.Status);
        }

        Console.Read();*/

        public static string GetGPUName()
        {
            List<DeviceInfo> devices = new List<DeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity WHERE PNPClass = 'Display'"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new DeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description"),
                (string)device.GetPropertyValue("Status")
                ));

            }

            collection.Dispose();
            return devices.First().Description;
        }
        

        public static bool GetStatus()
        {
            var devices = GetDevices();

            foreach(var device in devices)
            {
                if (device.Status != "OK")
                {
                    return false;
                }
            }
            return true;
        }

        static List<DeviceInfo> GetDevices()
        {
            List<DeviceInfo> devices = new List<DeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new DeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description"),
                (string)device.GetPropertyValue("Status")               
                ));
                
            }
            
            collection.Dispose();
            return devices;
        }
    }

    class DeviceInfo
    {
        public DeviceInfo(string deviceID, string pnpDeviceID, string description, string status)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
            this.Status = status;
        }
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
        public string Status { get; private set; }
    }
}
