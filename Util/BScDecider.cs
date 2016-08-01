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
    public partial class BScDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public BScDecider()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void BScDecider_Load(object sender, EventArgs e)
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
                    BScIssue bscissue = new BScIssue();
                    bscissue.Show();
                }

                else if (e.Result.Text == "add")
                {
                    BScAddBook bscaddbook = new BScAddBook();
                    bscaddbook.Show();
                }

                else if (e.Result.Text == "return")
                {
                    BScReturnBook bscreturnbook = new BScReturnBook();
                    bscreturnbook.Show();
                }

                else if (e.Result.Text == "delete")
                {
                    BScDelBook bscdelbook = new BScDelBook();
                    bscdelbook.Show();
                }

                else if (e.Result.Text == "details")
                {
                    BScDetofIssuedBooks bscdetofissuedbooks = new BScDetofIssuedBooks();
                    bscdetofissuedbooks.Show();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "BScDecider")
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
