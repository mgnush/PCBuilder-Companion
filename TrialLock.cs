using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Builder_Companion
{
    public static class TrialLock
    {
        public static DateTime GetNistTime()
        {
            DateTime dateTime = DateTime.MinValue;

            try
            {
                var client = new TcpClient("time.nist.gov", 13);
                using (var streamReader = new StreamReader(client.GetStream()))
                {
                    var response = streamReader.ReadToEnd();
                    var utcDateTimeString = response.Substring(7, 17);
                    dateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                }
            } catch
            {

            }
          

            return dateTime;
        }
    }
}
