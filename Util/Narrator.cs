using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SpeechLib;
using System.Speech.Recognition;

namespace Utility
{
    public partial class Narrator : Form
    {
        SpVoice speak = new SpVoice();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public Narrator()
        {
            InitializeComponent();
        }

        public Narrator(string str)
        {
            InitializeComponent();
            richTextBox1.Text = str;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = cd.Color;
            }
        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mail m = new Mail(richTextBox1.Text);
            m.Show();
            this.Close();
        }

        private void translateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Translator tr = new Translator(richTextBox1.Text);
            tr.Show();
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Narrator_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "speak", "resume", "wait", "open", "clear", "close" });

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
                if (e.Result.Text == "speak")
                {
                    try
                    {
                        speak.Speak(richTextBox1.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Faliure");
                    }
                }

                else if (e.Result.Text == "resume")
                {
                    try
                    {
                        speak.Resume();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Faliure");
                    }
                }

                else if (e.Result.Text == "wait")
                {
                    try
                    {
                        speak.Pause();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Faliure");
                    }
                }

                else if (e.Result.Text == "open")
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Text Files(*.txt)|*.txt";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Text = File.ReadAllText(ofd.FileName);
                    }
                }

                else if (e.Result.Text == "clear")
                {
                    richTextBox1.Clear();
                }

                else if (e.Result.Text == "close")
                {
                    this.Dispose();
                    sre.Dispose();
                    speak.Pause();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failure");
            }
        }
    }
}
