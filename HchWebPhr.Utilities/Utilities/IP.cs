using System;
using System.Net;
using System.Web;
using NetTools;

namespace HchWebPhr.Utilities
{
    public static class IP
    {
        public static string GetAddress()
        {
            var context = HttpContext.Current;
            
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static bool IsLocalAddress()
        {
            
            IPAddressRange[] ipRanges = new IPAddressRange[] {
                new IPAddressRange {
                    Begin = IPAddress.Parse("10.0.0.0"),
                    End = IPAddress.Parse("10.255.255.255")
                },
                new IPAddressRange {
                    Begin = IPAddress.Parse("172.16.0.0"),
                    End = IPAddress.Parse("172.31.255.255")
                },
                new IPAddressRange {
                    Begin = IPAddress.Parse("192.168.0.0"),
                    End = IPAddress.Parse("192.168.255.255")
                }
            };
            string addr = GetAddress();
#if DEBUG && FAKE_IP
            IPAddress requestIP = IPAddress.Parse("150.70.188.166");
#else
            IPAddress requestIP = IPAddress.Parse(addr);
#endif
            //requestIP = requestIP.MapToIPv4();
            foreach (IPAddressRange ipRange in ipRanges)
            {
                if (ipRange.Contains(requestIP))
                {
                    return true;
                }
            }
            return IPAddress.IsLoopback(requestIP);
            //return false;
        }

        public static string GetRoutes()
        {
            var context = HttpContext.Current;
            string ipRoutes = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (String.IsNullOrEmpty(ipRoutes))
            {
                ipRoutes = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipRoutes;
        }
    }
}