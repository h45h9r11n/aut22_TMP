using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;


namespace sys_doc
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Where do you want to save the file to update?");
            string pathDir = Console.ReadLine();
            if (!System.IO.File.Exists(pathDir))
                System.IO.Directory.CreateDirectory(pathDir);
            File.Copy(@"C:\Users\hanhnguyen26\source\repos\secur\secur\bin\Debug\secur.exe", pathDir + @"\secur.exe", true);
            string pathFile = pathDir + @"/sys.tat";
            if (!File.Exists(pathFile))
            {
                using (StreamWriter sw = File.CreateText(pathFile))
                {
                    sw.WriteLine("MachineName: {0}", Environment.MachineName);
                    sw.WriteLine("OSVersion: {0}", Environment.OSVersion.ToString());
                    sw.WriteLine("Processors: {0}", Environment.ProcessorCount.ToString());
                    double totalCapacity = 0;
                    ObjectQuery objectQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
                    ManagementObjectSearcher searcher = new
                    ManagementObjectSearcher(objectQuery);
                    ManagementObjectCollection vals = searcher.Get();

                    foreach (ManagementObject val in vals)
                    {
                        totalCapacity += System.Convert.ToDouble(val.GetPropertyValue("Capacity"));
                    }
                    sw.WriteLine("RAM: {0}", (totalCapacity / 1073741824).ToString());
                    sw.WriteLine("SystemDirectory: {0}", Environment.SystemDirectory);
                    string[] drives = Environment.GetLogicalDrives();
                    sw.WriteLine("GetLogicalDrives: {0}", String.Join(", ", drives));
                }
            }

            Directory.SetCurrentDirectory(pathDir);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pathDir + @"\secur.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "1";
            Process.Start(startInfo);

            ASCIIEncoding ByteConverter = new ASCIIEncoding();

            string dataString = File.ReadAllText(pathFile);

            // Create byte arrays to hold original, encrypted, and decrypted data.
            byte[] originalData = ByteConverter.GetBytes(dataString);
            byte[] signedData;

            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            string publicKey = RSAalg.ToXmlString(false);
            signedData = RSAalg.SignData(originalData, SHA256.Create());

            string keyName = @"HKEY_CURRENT_USER\SOFTWARE\Nguyen";
            Registry.SetValue(keyName, "Signature", signedData);
            Registry.SetValue(keyName, "originalData", originalData);
            Registry.SetValue(keyName, "publicKey", publicKey);


            Animation spin = new Animation();
            string loadingText = "Working....";

            Console.CursorVisible = false;

            for (int i = 0; i < 100; i++)
            {
                spin.Turn(loadingText);
                spin.SequencedMatrix(2, 0, 10, 10);
                spin.LoadingBar(loadingText, 12, 0);
                spin.Ready();
            }
            Console.WriteLine("\nUpdated succesfully.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}
