using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Counter
{
    public partial class Form1 : Form
    {
        private int TimesLimit = 5;
        
        public bool CheckLitmits = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void EnterUsername_Click(object sender, EventArgs e)
        {
            string PathLogFile = @"C:\Users\hanhnguyen26\Documents\logfile.txt";

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
                    if (int.Parse(Limits[Index + 1]) == 0)
                    {
                        MessageBox.Show("No tries left");
                        Form2 Uninstall = new Form2();
                        Uninstall.Show();
                    }
                    else
                    {
                        Limits[Index + 1] = (int.Parse(Limits[Index + 1]) - 1).ToString();
                        MessageBox.Show($"{Limits[Index + 1]} tries left");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Welcome to my application");
                    Limits.Add(username.Text);
                    Limits.Add(TimesLimit.ToString());
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
