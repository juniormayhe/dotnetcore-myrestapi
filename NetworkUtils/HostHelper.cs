using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static System.Diagnostics.Trace;

namespace NetworkUtils
{
    public class HostHelper
    {
        public static void OutputHostnameInfo(string applicationTitle)
        {
            var hostname = Dns.GetHostName();
            var host = Dns.GetHostEntry(hostname);
            string ip = "";
            try
            {
                var lista = new List<IPAddress>(host.AddressList);
                ip = lista.Where(x => x.ToString().Length < 16).ElementAt(0).ToString();
            }
            catch
            {
                ip = host.AddressList[0].ToString();
            }

            WriteLine($"\n\n- API: {applicationTitle}");
            WriteLine($"- Current hostname in docker: {hostname}");
            WriteLine($"- Current ip in docker: {ip}");
            WriteLine($"- You must call this URL with ip http://{ip}{(applicationTitle=="IdentityAPI"? "/.well-known/openid-configuration":"")}\n\n");
        }
    }
}
