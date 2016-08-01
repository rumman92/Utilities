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
    public partial class DDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public DDecider()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void DDecider_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "encryptor", "decryptor", "delete", "close" });

            GrammarBuilder gb = new GrammarBuilder(ch);
            Grammar g = new Grammar(gb);

            sre.LoadGrammarAsync(g);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "encryptor")
            {
                DataEnCryptor dec = new DataEnCryptor();
                dec.Show();
            }

            else if (e.Result.Text == "decryptor")
            {
                DDePass ddp = new DDePass();
                ddp.Show();
            }

            else if (e.Result.Text == "delete")
            {
                DeleteKeyandIV dkiv = new DeleteKeyandIV();
                dkiv.Show();
            }

            else if (e.Result.Text == "close" && ActiveForm.Name == "DDecider")
            {
                this.Close();
                sre.Dispose();
            }
        }
    }
}
