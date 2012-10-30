using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiFuscate_Beta
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 100)
            {
                progressBar1.Value += 1;
                if (progressBar1.Value <= 49){label1.Text = "Connecting to online services...";}
                else if (progressBar1.Value == 50){label1.Text = "Preparing Algorithms...";}
                else if (progressBar1.Value == 75){label1.Text = "Checking that you have the latest version...";}
            }
            else
            { lic(); }
        }

        private void splash_Load(object sender, EventArgs e)
        {
                timer1.Start();
        }
        private void lic()
        {
            timer1.Stop();
            if (NiFuscate_Beta.Properties.Settings.Default.liceshown == false)
            {
                licenseagreement lic = new licenseagreement();
                lic.Show();
                this.Dispose(false);
            }
            else if (NiFuscate_Beta.Properties.Settings.Default.licdecline == true)
            {
                licenseagreement lic = new licenseagreement();
                lic.Show();
                this.Dispose(false);
            }
            else if (NiFuscate_Beta.Properties.Settings.Default.licagree == true && NiFuscate_Beta.Properties.Settings.Default.liceshown == true)
            {
                Form1 home = new Form1(); home.Show(); this.Dispose(false);
            }
        }
    }
}
