
using Microsoft.Win32;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;

namespace secur
{
    class Program
    {
        static void SetFileSecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            DirectoryInfo dInfo = new DirectoryInfo(FileName);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
            dInfo.SetAccessControl(dSecurity);
        }

        static void RemoveFileSecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {

            DirectoryInfo dInfo = new DirectoryInfo(FileName);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));
            dInfo.SetAccessControl(dSecurity);
        }

        static void Main(string[] args)
        {
            string FileName = @".\sys.tat";

            System.Security.Principal.SecurityIdentifier SID = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
            System.Security.Principal.NTAccount NTAccount = SID.Translate(typeof(System.Security.Principal.NTAccount)) as System.Security.Principal.NTAccount;
            string Everyone = NTAccount.ToString();
            if (args.Length == 1)
            {
                SetFileSecurity(FileName, Everyone, FileSystemRights.FullControl, AccessControlType.Deny);
            } else
            {
                Console.WriteLine("Enter address of registry: ");
                string keyName = Console.ReadLine();
                byte[] signedData = (byte[]) Registry.GetValue(keyName, "Signature", new byte[] {});
                byte[] originalData = (byte[]) Registry.GetValue(keyName, "originalData", new byte[] { });
                string publicKey = (string) Registry.GetValue(keyName, "publicKey", new byte[] { });

                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.FromXmlString(publicKey);
                Console.WriteLine("OK");
                if (RSAalg.VerifyData(originalData, SHA256.Create(), signedData)){
                    Console.WriteLine("Correct");
                    RemoveFileSecurity(FileName, Everyone, FileSystemRights.FullControl, AccessControlType.Deny);
                }
                Console.WriteLine();
            }
        }


    }
}
