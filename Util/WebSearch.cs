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
    public partial class WebSearch : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public WebSearch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void WebSearch_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "go", "backward", "forward", "close" });

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
                if (e.Result.Text == "go")
                {
                    string query = textBox1.Text;
                    string gurl = "www.google.com/search?q=" + query;
                    string burl = "www.bing.com/search?q=" + query;
                    webBrowser1.Navigate(gurl);
                    webBrowser2.Navigate(burl);
                }

                else if (e.Result.Text == "backward")
                {
                    webBrowser1.GoBack();
                    webBrowser2.GoBack();
                }

                else if (e.Result.Text == "forward")
                {
                    webBrowser1.GoForward();
                    webBrowser2.GoForward();
                }

                else if (e.Result.Text == "close")
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
