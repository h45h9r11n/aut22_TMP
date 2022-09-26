using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Counter
{
    public partial class Form1 : Form
    {
        
        public bool CheckLitmits = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void EnterUsername_Click(object sender, EventArgs e)
        {
            string PathLogFile = @"C:\Users\hanhnguyen26\Documents\TimeLimited.txt";

            Assembly assembly = Assembly.GetExecutingAssembly();
            Uri uri = new Uri(Path.GetDirectoryName(assembly.CodeBase));

            char[] DelimiterChars = { '\n' };
            List<string> Limits = File.ReadAllText(PathLogFile).Split(DelimiterChars).ToList();
            Limits[0] = uri.ToString();
            int Index = Limits.FindIndex(x => x.StartsWith(username.Text));
            if (!CheckLitmits)
            {
                
                if (Index >= 0)
                {
                    DateTime TimeNow = DateTime.Now;
                    DateTime TimeEnd = Convert.ToDateTime(Limits[Index + 1], CultureInfo.InvariantCulture);
                    double Interval = (TimeEnd - TimeNow).TotalSeconds;
           
                    if (Interval < 0)
                    {
                        MessageBox.Show("Timeup");
                        Form2 Uninstall = new Form2();
                        Uninstall.Show();
                    }
                    else
                    {
                        MessageBox.Show($"{Interval} second(s) left");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Welcome to my application");
                    DateTime Time = DateTime.Now;
                    Time = Time.AddSeconds(30);
                    Limits.Add(username.Text);
                    Limits.Add(Time.ToString());
                }
                var escapedLines = Limits.Select(l => l.Replace("\n", "").Replace("\r", "")).ToList();
                File.WriteAllLines(PathLogFile, escapedLines);
                CheckLitmits = true;
            }
            else
            { 
                this.Close();
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
