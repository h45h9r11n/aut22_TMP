using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows;

namespace CustomInstaller
{
    public class CustomActions
    {
        
        [CustomAction]
        public static ActionResult CustomAction1(Session session)
        {
            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult CustomAction2(Session session)
        {
            string PathLogFile = @"C:\Users\hanhnguyen26\Documents\logfile.txt";

            if (File.Exists(PathLogFile))
            {
                string[] lines = File.ReadAllText(PathLogFile).Split('\n');
                if (lines[0] != "")
                {
                    MessageBox.Show($"The program had been installed at {lines[0]}");
                }
                
            } else
            {
                File.Create(PathLogFile);
            }

            return ActionResult.Success;
        }
    }
}
