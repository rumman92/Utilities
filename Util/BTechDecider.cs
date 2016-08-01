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
    public partial class BTechDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public BTechDecider()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            
        }

        private void BTechDecider_Load(object sender, EventArgs e)
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
                    BTechIssue btechissue = new BTechIssue();
                    btechissue.Show();
                } 

                else if (e.Result.Text == "add")
                {
                    BTechAddBook btechaddbook = new BTechAddBook();
                    btechaddbook.Show();
                } 

                else if (e.Result.Text == "return")
                {
                    BTechReturnBook btechreturnbook = new BTechReturnBook();
                    btechreturnbook.Show();
                } 

                else if (e.Result.Text == "delete")
                {
                    BTechDelBook btechdelbook = new BTechDelBook();
                    btechdelbook.Show();
                } 

                else if (e.Result.Text == "details")
                {
                    BTechDetOfIssuedBooks btechdetofissuebooks = new BTechDetOfIssuedBooks();
                    btechdetofissuebooks.Show();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name=="BTechDecider")
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
