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
    public partial class MArchDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public MArchDecider()
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MArchDecider_Load(object sender, EventArgs e)
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
                    MArchIssue marchissue = new MArchIssue();
                    marchissue.Show();
                }

                else if (e.Result.Text == "add")
                {
                    MArchAddBook marchaddbook = new MArchAddBook();
                    marchaddbook.Show();
                }

                else if (e.Result.Text == "return")
                {
                    MArchReturnBook marchreturnbook = new MArchReturnBook();
                    marchreturnbook.Show();
                }

                else if (e.Result.Text == "delete")
                {
                    MArchDelBook marchdelbook = new MArchDelBook();
                    marchdelbook.Show();
                }

                else if (e.Result.Text == "details")
                {
                    MArchDetOfIssuedBooks marchdetofissuedbooks = new MArchDetOfIssuedBooks();
                    marchdetofissuedbooks.Show();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "MArchDecider")
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
