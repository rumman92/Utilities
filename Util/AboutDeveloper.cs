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
    public partial class AboutDeveloper : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public AboutDeveloper()
        {
            InitializeComponent();
        }

        private void AboutDeveloper_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "close" });

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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:rumman.siddiqui@live.in");
        }
    }
}
