using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Counter
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BuyALicense_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.google.com");
        }

        private void Uninstall_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "msiexec.exe";
            p.StartInfo.Arguments = "/x {2163366D-5E61-4DCB-802B-B45CC6EE1BFF}";
            p.Start();
        }

    }
}
