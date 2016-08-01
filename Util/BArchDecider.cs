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
    public partial class BArchDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public BArchDecider()
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

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void BArchDecider_Load(object sender, EventArgs e)
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
                    BArchIssue barch = new BArchIssue();
                    barch.Show();
                }

                else if (e.Result.Text == "add")
                {
                    BArchAddBook barchaddbook = new BArchAddBook();
                    barchaddbook.Show();
                }

                else if (e.Result.Text == "return")
                {
                    BArchReturnBook barchreturnbook = new BArchReturnBook();
                    barchreturnbook.Show();
                }

                else if (e.Result.Text == "delete")
                {
                    BArchDelBook barchdelbook = new BArchDelBook();
                    barchdelbook.Show();
                }

                else if (e.Result.Text == "details")
                {
                    BArchDetofIssuedBooks barchdetofissuedbooks = new BArchDetofIssuedBooks();
                    barchdetofissuedbooks.Show();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "BArchDecider")
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
