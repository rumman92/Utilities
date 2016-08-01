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
    public partial class MTechDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public MTechDecider()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void MTechDecider_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "issue", "add", "return", "delete", "details", "close" });

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
                if (e.Result.Text == "issue")
                {
                    MTechIssue missue = new MTechIssue();
                    missue.Show();
                }

                else if (e.Result.Text == "add")
                {
                    MTechAddBook maddbook = new MTechAddBook();
                    maddbook.Show();
                }

                else if (e.Result.Text == "return")
                {
                    MTechReturnBook mretbook = new MTechReturnBook();
                    mretbook.Show();
                }

                else if (e.Result.Text == "delete")
                {
                    MTechDelBook mdelbook = new MTechDelBook();
                    mdelbook.Show();
                }

                else if (e.Result.Text == "details")
                {
                    MTechDetOfIssuedBooks mdetofissuedbooks = new MTechDetOfIssuedBooks();
                    mdetofissuedbooks.Show();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "MTechDecider")
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
    }
}
