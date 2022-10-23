
using Microsoft.Win32;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace secur
{
    class Program
    {
        static void SetFileSecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings.
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        static void RemoveFileSecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings.
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        static bool CheckSignature()
        {
            return true;
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
                byte[] Signature = (byte[])Registry.GetValue(keyName, "Signature", new byte[] {});
                if (CheckSignature()){
                    RemoveFileSecurity(FileName, Everyone, FileSystemRights.FullControl, AccessControlType.Deny);
                }
                   
            }
        }


    }
}
