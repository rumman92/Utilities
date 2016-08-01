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
    public partial class DDePass : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public DDePass()
        {
            InitializeComponent();
        }

        private void enter()
        {
            if (textBox1.Text == "rumman-siddiqui-92-@-gmail-com")
            {
                DataDeCryptor ddc = new DataDeCryptor();
                ddc.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Pass Key", "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DDePass_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "enter", "close" });

            GrammarBuilder gb = new GrammarBuilder(ch);
            Grammar g = new Grammar(gb);

            sre.LoadGrammarAsync(g);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "enter")
            {
                enter();
            }

            else if (e.Result.Text == "close")
            {
                this.Close();
                sre.Dispose();
            }
        }
    }
}
