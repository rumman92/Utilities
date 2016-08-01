using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using WindowsFormsToolkit.Controls;
using System.Speech.Recognition;

namespace Utility
{
    public partial class Web_Scrapper : Form
    {
        string str = string.Empty;
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public Web_Scrapper()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void Web_Scrapper_Load(object sender, EventArgs e)
        {
            CueTextExtender cue = new CueTextExtender();
            cue.SetCueText(textBox1, "http://www.example.com");

            Choices ch = new Choices();
            ch.Add(new string[] { "scrap", "HTML", "Email", "URL", "script", "readable", "web page", "close" });

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
                if (e.Result.Text == "scrap")
                {
                    try
                    {
                        WebRequest req = WebRequest.Create(textBox1.Text);
                        WebResponse resp = req.GetResponse();

                        StreamReader sr = new StreamReader(resp.GetResponseStream());
                        str = sr.ReadToEnd();

                        richTextBox1.Text = str;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }

                else if (e.Result.Text == "HTML")
                {
                    richTextBox1.Clear();
                    richTextBox1.Text = str;
                }

                else if (e.Result.Text == "Email")
                {
                    richTextBox1.Clear();
                    string eml = @"^((?<emails>" + @"[a-zA-Z0-9-_]" + @"+(\." + @"[a-zA-Z0-9-_]" + @"+)*@" + @"[a-zA-Z0-9-_]" + @"+(\." + @"[a-zA-Z0-9-_]" + @"+)*)*(.|\n|\r\n)*?)+$";
                    Regex reg = new Regex(eml);

                    foreach (Match m in reg.Matches(str))
                    {
                        richTextBox1.Text = richTextBox1.Text + m.Value + "\n";
                    }
                }

                else if (e.Result.Text == "URL")
                {
                    try
                    {
                        richTextBox1.Clear();
                        string pat = @"\b[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)\b";

                        Regex reg = new Regex(pat);

                        foreach (Match m in reg.Matches(str))
                        {
                            richTextBox1.Text = richTextBox1.Text + m.Value + "\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else if (e.Result.Text == "script")
                {
                    try
                    {
                        richTextBox1.Clear();
                        string sc = @"<script.*?</script>";

                        Regex reg = new Regex(sc);

                        foreach (Match m in reg.Matches(str))
                        {
                            richTextBox1.Text = richTextBox1.Text + m.Value + "\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (e.Result.Text == "readable")
                {
                    try
                    {
                        string read;
                        read = Regex.Replace(str, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        read = Regex.Replace(read, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        read = Regex.Replace(read, "</?[a-z][a-z0-9]*[^<>]*>", "");
                        read = Regex.Replace(read, "<!--(.|\\s)*?-->", "");
                        read = Regex.Replace(read, "<!(.|\\s)*?>", "");
                        read = Regex.Replace(read, "[\t\r\n]", " ");

                        richTextBox1.Text = read;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else if (e.Result.Text == "web page")
                {
                    WebPage wp = new WebPage(textBox1.Text);
                    wp.Show();
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