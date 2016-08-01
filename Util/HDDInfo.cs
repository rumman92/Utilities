using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Speech.Recognition;

namespace Utility
{
    public partial class HDDInfo : Form
    {
        SpeechRecognitionEngine sre=new SpeechRecognitionEngine();

        public HDDInfo()
        {
            InitializeComponent();
        }

        private void HDDInfo_Load(object sender, EventArgs e)
        {
            try
            {
                ManagementObjectSearcher mosearcher = new ManagementObjectSearcher("SELECT * FROM WIN32_DISKDRIVE");

                foreach (ManagementObject mobject in mosearcher.Get())
                {
                    comboBox1.Items.Add(mobject["Model"].ToString());
                }


                Choices ch = new Choices();
                ch.Add(new string[] { "close" });

                GrammarBuilder gb = new GrammarBuilder(ch);
                Grammar g = new Grammar(gb);

                sre.LoadGrammarAsync(g);
                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (e.Result.Text == "close")
                {
                    this.Close();
                    sre.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failure");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ManagementObjectSearcher mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Model = '" + comboBox1.SelectedItem + "'");

                foreach (ManagementObject moDisk in mosDisks.Get())
                {
                    try
                    {

                        label2.Text = "Type: " + moDisk["MediaType"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Media Type not found");
                        label2.Text = "Type: ";
                    }

                    try
                    {
                        label4.Text = "Model: " + moDisk["Model"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Model not found");
                        label4.Text = "Model: ";
                    }
                    try
                    {
                        label5.Text = "Serial: " + moDisk["SerialNumber"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Serial Number not found");
                        label5.Text = "Serial: ";
                    }
                    try
                    {
                        label6.Text = "Interface: " + moDisk["InterfaceType"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Interface not found");
                        label6.Text = "Interface: ";
                    }
                    try
                    {
                        label7.Text = "Capacity: " + moDisk["Size"].ToString() + " bytes (" + Math.Round(((((double)Convert.ToDouble(moDisk["Size"]) / 1024) / 1024) / 1024), 2) + " GB)";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Capacity not found");
                        label7.Text = "Capacity: ";
                    }
                    try
                    {
                        label8.Text = "Partitions: " + moDisk["Partitions"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Partitions not found");
                        label8.Text = "Partitions: ";
                    }
                    try
                    {
                        label16.Text = "Signature: " + moDisk["Signature"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Signature not found");
                        label16.Text = "Signature: ";
                    }
                    try
                    {
                        label17.Text = "Firmware: " + moDisk["FirmwareRevision"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Firmware not found");
                        label17.Text = "Firmware: ";
                    }
                    try
                    {
                        label9.Text = "Cylinders: " + moDisk["TotalCylinders"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Cylinders not found");
                        label9.Text = "Cylinders: ";
                    }
                    try
                    {
                        label11.Text = "Sectors: " + moDisk["TotalSectors"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sectors not found");
                        label11.Text = "Sectors: ";
                    }
                    try
                    {
                        label10.Text = "Heads: " + moDisk["TotalHeads"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Heads not found");
                        label10.Text = "Heads: ";
                    }
                    try
                    {
                        label12.Text = "Tracks: " + moDisk["TotalTracks"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tracks not found");
                        label12.Text = "Tracks: ";
                    }
                    try
                    {
                        label13.Text = "Bytes per Sector: " + moDisk["BytesPerSector"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Bytes per Sector not found");
                        label13.Text = "Bytes per Sector: ";
                    }
                    try
                    {
                        label14.Text = "Sectors per Track: " + moDisk["SectorsPerTrack"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sectors per Track not found");
                        label14.Text = "Sectors per Track: ";
                    }
                    try
                    {
                        label15.Text = "Tracks per Cylinder: " + moDisk["TracksPerCylinder"].ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Tracks per Cylinder not found");
                        label15.Text = "Tracks per Cylinder: ";
                    }
                }
            }

            catch (Exception)
            {

            }
        }
    }
}
        
