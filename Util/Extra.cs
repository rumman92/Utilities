using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace Utility
{
    public partial class Extra : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        public Extra()
        {
            InitializeComponent();
        }

        private void Extra_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "notepad", "wordpad", "paint", "calculator", "media player", "command", "terminate", "close" });

            GrammarBuilder gb = new GrammarBuilder(ch);
            Grammar g = new Grammar(gb);

            sre = new SpeechRecognitionEngine();
            sre.LoadGrammarAsync(g);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
            sre.RecognizeAsync(RecognizeMode.Multiple);

        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (e.Result.Text == "notepad")
                {
                    System.Diagnostics.Process.Start(@"C:\Windows\System32\NOTEPAD.EXE");
                }

                else if (e.Result.Text == "wordpad")
                {
                    System.Diagnostics.Process.Start(@"C:\Program Files\Windows NT\Accessories\WORDPAD.EXE");
                }

                else if (e.Result.Text == "paint")
                {
                    System.Diagnostics.Process.Start(@"C:\Windows\System32\MSPAINT.EXE");
                }

                else if (e.Result.Text == "calculator")
                {
                    System.Diagnostics.Process.Start(@"C:\Windows\System32\CALC.EXE");
                }

                else if (e.Result.Text == "media player")
                {
                    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe");
                }

                else if (e.Result.Text == "command")
                {
                    System.Diagnostics.Process.Start(@"C:\Windows\System32\Cmd.Exe");
                }

                else if (e.Result.Text == "terminate")
                {
                    System.Diagnostics.Process[] prcs = System.Diagnostics.Process.GetProcesses();
                    foreach (System.Diagnostics.Process pr in prcs)
                    {
                        if (pr.ProcessName == "notepad")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "wordpad")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "mspaint")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "calc")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "wmplayer")
                        {
                            pr.Kill();
                        }
                        else if (pr.ProcessName == "cmd")
                        {
                            pr.Kill();
                        }
                    }
                }

                else if (e.Result.Text == "close")
                {
                    this.Close();
                    sre.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
        }
    }
}
