/*
 * TrialLock.cs
 * 
 * @Author  Magnus Hjorth
 * 
 * File Description: This class provides a means to restrict the application after a certain date.
 * Depending on the caller, parts of or the entire program may be halted after the date specified
 * by the caller.
 */

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
        /// <summary>
        /// Gets the NIST time from time.nist.gov.
        /// </summary>
        /// <returns>The NIST DateTime. If connection failed, DateTime.MinValue</returns>
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
