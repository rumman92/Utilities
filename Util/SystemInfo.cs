using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Speech.Recognition;

namespace Utility
{
    public partial class SystemInfo : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public SystemInfo()
        {
            InitializeComponent();
        }

        private void SystemInfo_Load(object sender, EventArgs e)
        {
            label2.Text = "Machine Name: " + Environment.MachineName;
            label3.Text = "Machine OS Version: " + Environment.OSVersion.VersionString;
            label4.Text = "Process Exit Code: " + Environment.ExitCode.ToString();
            label5.Text = "Number of Processor: " + Environment.ProcessorCount;
            label6.Text = "System Directory: " + Environment.SystemDirectory;
            label7.Text = "Milliseconds Since System has Started: " + (Environment.TickCount).ToString();
            label8.Text = "User Domain Name: " + Environment.UserDomainName;
            label9.Text = "Name of User: " + Environment.UserName;
            label10.Text = "Amount of Physical Memory Mapped: " + Environment.WorkingSet.ToString();

            DriveInfo di = new DriveInfo("C");
            string str = "Name: " + di.Name;
            str += "\n\nAvailable Free Space: " + di.AvailableFreeSpace.ToString();
            str += "\n\nDrive Format: " + di.DriveFormat;
            str += "\n\nTotal Size: " + di.TotalSize.ToString();

            label11.Text = str;


            Choices ch = new Choices();
            ch.Add(new string[] { "disk", "directX", "clean up", "services", "event viewer", "configuration", "performance", "close", "terminate" });

            GrammarBuilder gb = new GrammarBuilder(ch);
            Grammar g = new Grammar(gb);

            sre.LoadGrammarAsync(g);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (e.Result.Text == "disk")
                {
                    HDDInfo hinfo = new HDDInfo();
                    hinfo.Show();
                }

                else if (e.Result.Text == "directX")
                {
                    System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\dxdiag.exe");
                }

                else if (e.Result.Text == "clean up")
                {
                    System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\CLEANMGR");
                }
                else if (e.Result.Text == "services")
                {
                    System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\DCOMCNFG.EXE");
                }

                else if (e.Result.Text == "event viewer")
                {
                    System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\eventvwr");
                }

                else if (e.Result.Text == "configuration")
                {
                    try
                    {
                        System.Diagnostics.Process.Start(@"C:\Windows\System32\msconfig.EXE");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else if (e.Result.Text == "performance")
                {
                    System.Diagnostics.Process.Start(@"C:\Windows\System32\perfmon.exe");
                }

                else if (e.Result.Text == "terminate")
                {
                    System.Diagnostics.Process[] prcs = System.Diagnostics.Process.GetProcesses();
                    foreach (System.Diagnostics.Process pr in prcs)
                    {
                        if (pr.ProcessName == "dxdiag")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "cleanmgr")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "mmc")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "msconfig")
                        {
                            pr.Kill();
                        }
                    }
                }
                
                else if (e.Result.Text == "close" && ActiveForm.Name == "SystemInfo")
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text = "Milliseconds Since System has Started: " + (Environment.TickCount).ToString();
        }
    }
}
