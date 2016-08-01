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
    public partial class WebDecider : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public WebDecider()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void WebDecider_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "search", "scraper", "close" });

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
                if (e.Result.Text == "search")
                {
                    WebSearch wsrch = new WebSearch();
                    wsrch.Show();

                }

                else if (e.Result.Text == "scraper")
                {
                    Web_Scrapper ws = new Web_Scrapper();
                    ws.Show();

                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "WebDecider")
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
