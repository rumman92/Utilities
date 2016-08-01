using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WindowsFormsToolkit.Controls;
using System.Speech.Recognition;

namespace Utility
{
    public partial class MConsole : Form
    {
        int c = 0;

        SpeechRecognitionEngine sre=new SpeechRecognitionEngine();

        public MConsole()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void MConsole_Load(object sender, EventArgs e)
        {
            timer1.Stop();

            label1.Focus();
            
            CueTextExtender cue = new CueTextExtender();
            cue.SetCueText(textBox1, "Numeric value only !");

            Choices ch = new Choices();
            ch.Add(new string[] { "start", "close" });

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
                if (e.Result.Text == "start")
                {
                    try
                    {
                        timer1.Start();
                        timer1.Interval = (Convert.ToInt32(textBox1.Text)) * 1000;
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
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
                MessageBox.Show(ex.Message, "Failure");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics gr = Graphics.FromImage(bmp);
                gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);

                DirectoryInfo di = Directory.CreateDirectory(Environment.CurrentDirectory + @"\Images");
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

                string path = string.Format(di.FullName + @"\img[{0}].jpeg", c);

                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

                c++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void close(object sender, FormClosedEventArgs e)
        {
            this.Close();
            sre.Dispose();
        }
    }
}
