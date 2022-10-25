using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedTeamDev
{
    class GeneralInfo
    {
        public string oSystem; //string variable for storing operating system
        public string uName; //string variable for storing username
        public string ipv4address; //string variable for ipv4adress
        public string hostName;
        public string CPU;

        public GeneralInfo()
        {
            oSystem = Environment.OSVersion.ToString();
            uName = Environment.UserName;
            hostName = Dns.GetHostName();
            ipv4address = Dns.GetHostByName(hostName).AddressList[1].ToString();
            CPU = Environment.ProcessorCount.ToString();
        }


    }
}
