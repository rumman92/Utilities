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
    public partial class MScDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public MScDecider()
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

        private void MScDecider_Load(object sender, EventArgs e)
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
                    MScIssue mscissue = new MScIssue();
                    mscissue.Show();
                }

                else if (e.Result.Text == "add")
                {
                    MScAddBook mscaddbook = new MScAddBook();
                    mscaddbook.Show();
                }

                else if (e.Result.Text == "return")
                {
                    MScReturnBook mscreturnbook = new MScReturnBook();
                    mscreturnbook.Show();
                }

                else if (e.Result.Text == "delete")
                {
                    MScDelBook mscdelbook = new MScDelBook();
                    mscdelbook.Show();
                }

                else if (e.Result.Text == "details")
                {
                    MScDetOfIssuedBooks mscdetofissuedbooks = new MScDetOfIssuedBooks();
                    mscdetofissuedbooks.Show();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "MScDecider")
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
