using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NiFuscate_Beta
{
    public partial class licenseagreement : Form
    {
        public licenseagreement()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("http://creativecommons.org/licenses/by-sa/3.0/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NiFuscate_Beta.Properties.Settings.Default.licdecline = true;
            NiFuscate_Beta.Properties.Settings.Default.liceshown = true;
            NiFuscate_Beta.Properties.Settings.Default.Save();
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NiFuscate_Beta.Properties.Settings.Default.licdecline = false;
            NiFuscate_Beta.Properties.Settings.Default.liceshown = true;
            NiFuscate_Beta.Properties.Settings.Default.licagree = true;
            NiFuscate_Beta.Properties.Settings.Default.Save();
            Form1 home = new Form1();
            home.Show();
            this.Close();
        }
    }
}
