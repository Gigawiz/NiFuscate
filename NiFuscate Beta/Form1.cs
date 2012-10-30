using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace NiFuscate_Beta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD1 = new OpenFileDialog();
            // Set the file dialog to filter for graphics files. 
            OFD1.Filter ="JavaScript (*.js)|*.js";

            // Allow the user to select multiple images. 
            OFD1.Multiselect = true;
            OFD1.Title = "Select JavaScript Files to Obfuscate. Hold CTRL to select Multiple.";
            if (OFD1.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in OFD1.FileNames)
                {
                    listBox1.Items.Add(file);
                }
            }
            if (listBox1.Items.Count > 0)
            {
                button3.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about abt = new about();
            abt.Show();
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            licenseagreement lic = new licenseagreement();
            lic.Show();
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=V4297V3548FVS");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //conversion starts here
            if (listBox1.Items.Count > 0)
            {
                if (!String.IsNullOrEmpty(NiFuscate_Beta.Properties.Settings.Default.saveloc))
                {
                    foreach (string enc in listBox1.Items)
                    {
                        uploadF(enc);
                        log.Items.Add("Successfully uploaded " + enc);
                        string url = "http://nicoding.com/api.php?app=nifuscate&lang=js&file=" + Path.GetFileName(enc) + "&dl=true";
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        WebResponse response = request.GetResponse();
                        System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("windows-1252"));
                        string newf = NiFuscate_Beta.Properties.Settings.Default.saveloc + @"\" + Path.GetFileName(enc);
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(newf);
                        sw.Write(sr.ReadToEnd());
                        sw.Close();
                        log.Items.Add("Successfully Obfuscated " + enc);
                        string newloc = NiFuscate_Beta.Properties.Settings.Default.saveloc + @"\" + Path.GetFileName(enc);
                        listBox2.Items.Add(newloc);

                    }
                    listBox1.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please set your Save Directory before trying to obfuscate!");
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    listBox1.Items.RemoveAt(listBox1.SelectedIndices[i]);
                }
            }
        }

        private void uploadF(string filePath)
        {
            try
            {
                string FTPAddress = "ftp://nicoding.com";
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(filePath));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("NiFuscate@webmasterseotools.com", "nifuscatebeta");
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                FileStream stream = File.OpenRead(filePath);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowNewFolderButton = true;
            FBD.Description = "Select a folder to save obfuscated files to.";
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                NiFuscate_Beta.Properties.Settings.Default.saveloc = FBD.SelectedPath;
                NiFuscate_Beta.Properties.Settings.Default.Save();
                if (listBox1.Items.Count > 0)
                {
                    button3.Enabled = true;
                }
            }
        }
    }
}
