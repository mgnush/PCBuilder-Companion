using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Builder_Companion
{
    public static class DMChecker
    {     

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

        public static string GetGPUDriver()
        {
            ManagementObjectSearcher objSearcher = new ManagementObjectSearcher("Select * from Win32_PnPSignedDriver WHERE DeviceClass = 'Display'");

            ManagementObjectCollection objectCollection = objSearcher.Get();

            string driver = "";

            foreach (ManagementObject obj in objectCollection)
            {
                driver = String.Format("{0}", obj["DriverVersion"]);
            }

            return driver;
        }
        
        public static string GetCPUName()
        {
            List<DeviceInfo> devices = new List<DeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity WHERE PNPClass = 'Processor'"))
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
            return devices.First().DeviceID;
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
            this.Driver = "";
        }

        public DeviceInfo(string deviceID, string pnpDeviceID, string description, string status, string driver)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
            this.Status = status;
            this.Driver = driver;
        }

        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public string Driver { get; private set; }
    }
}
