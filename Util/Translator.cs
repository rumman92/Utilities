using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.IO;
using Microsoft;
using System.Speech.Recognition;

namespace Utility
{
    public partial class Translator : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public Translator()
        {
            InitializeComponent();
        }

        public Translator(string str)
        {
            InitializeComponent();
            richTextBox1.Text = str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void translate()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (richTextBox1.Text == string.Empty)
            {
                MessageBox.Show("Nothing to translate, please enter some text", "Missing");
            }
            else
            {

                string from = " ";
                string to = " ";
                //From
                switch (comboBox1.Text)
                {
                    case "Arabic": from = "ar";
                        break;
                    case "Czech": from = "cs";
                        break;
                    case "Danish": from = "da";
                        break;
                    case "German": from = "de";
                        break;
                    case "English": from = "en";
                        break;
                    case "Estonian": from = "et";
                        break;
                    case "Finnish": from = "fi";
                        break;
                    case "Dutch": from = "nl";
                        break;
                    case "Greek": from = "el";
                        break;
                    case "Hebrew": from = "he";
                        break;
                    case "Haitian Creole": from = "ht";
                        break;
                    case "Hindi": from = "hi";
                        break;
                    case "Hungarian": from = "hu";
                        break;
                    case "Indonesian": from = "id";
                        break;
                    case "Italian": from = "it";
                        break;
                    case "Japanese": from = "ja";
                        break;
                    case "Korean": from = "ko";
                        break;
                    case "Lithuanian": from = "lt";
                        break;
                    case "Latvian": from = "lv";
                        break;
                    case "Norwegian": from = "no";
                        break;
                    case "Polish": from = "pl";
                        break;
                    case "Portuguese": from = "pt";
                        break;
                    case "Romanian": from = "ro";
                        break;
                    case "Russian": from = "ru";
                        break;
                    case "Spanish": from = "es";
                        break;
                    case "Slovak": from = "sk";
                        break;
                    case "Slovene": from = "sl";
                        break;
                    case "Swedish": from = "sv";
                        break;
                    case "Thai": from = "th";
                        break;
                    case "Turkish": from = "tr";
                        break;
                    case "Ukranian": from = "uk";
                        break;
                    case "Vietnamese": from = "vi";
                        break;
                    case "Simplified Chinese": from = "zh-CHS";
                        break;
                    case "Traditional Chinese": from = "zh-CHT";
                        break;
                    default: errorProvider1.SetError(comboBox1, "Choose appropriate language");
                        break;
                }
                //To
                switch (comboBox2.Text)
                {
                    case "Arabic": to = "ar";
                        break;
                    case "Czech": to = "cs";
                        break;
                    case "Danish": to = "da";
                        break;
                    case "German": to = "de";
                        break;
                    case "English": to = "en";
                        break;
                    case "Estonian": to = "et";
                        break;
                    case "Finnish": to = "fi";
                        break;
                    case "Dutch": to = "nl";
                        break;
                    case "Greek": to = "el";
                        break;
                    case "Hebrew": to = "he";
                        break;
                    case "Haitian Creole": to = "ht";
                        break;
                    case "Hindi": to = "hi";
                        break;
                    case "Hungarian": to = "hu";
                        break;
                    case "Indonesian": to = "id";
                        break;
                    case "Italian": to = "it";
                        break;
                    case "Japanese": to = "ja";
                        break;
                    case "Korean": to = "ko";
                        break;
                    case "Lithuanian": to = "lt";
                        break;
                    case "Latvian": to = "lv";
                        break;
                    case "Norwegian": to = "no";
                        break;
                    case "Polish": to = "pl";
                        break;
                    case "Portuguese": to = "pt";
                        break;
                    case "Romanian": to = "ro";
                        break;
                    case "Russian": to = "ru";
                        break;
                    case "Spanish": to = "es";
                        break;
                    case "Slovak": to = "sk";
                        break;
                    case "Slovene": to = "sl";
                        break;
                    case "Swedish": to = "sv";
                        break;
                    case "Thai": to = "th";
                        break;
                    case "Turkish": to = "tr";
                        break;
                    case "Ukranian": to = "uk";
                        break;
                    case "Vietnamese": to = "vi";
                        break;
                    case "Simplified Chinese": to = "zh-CHS";
                        break;
                    case "Traditional Chinese": to = "zh-CHT";
                        break;
                    default: errorProvider1.SetError(comboBox2, "Choose appropriate language");
                        break;
                }

                try
                {
                    BingTranslator.LanguageServiceClient client = new Utility.BingTranslator.LanguageServiceClient("BasicHttpBinding_LanguageService");
                    richTextBox2.Text = client.Translate("ZSeBOiugOwnYef+LzHObm3d4IW7RnlNlmKdcHak62Qw=", richTextBox1.Text, from, to);

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Faliure");
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
                richTextBox2.SelectionFont = fd.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = cd.Color;
                richTextBox2.SelectionColor= cd.Color;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
            richTextBox2.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
            richTextBox2.Paste();
        }

        private void Translator_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "English";

            Choices ch = new Choices();
            ch.Add(new string[] { "translate", "open", "save", "clear", "close" });

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
                if (e.Result.Text == "translate")
                {
                    translate();
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

                else if (e.Result.Text == "save")
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Text Files(*.txt)|*.txt";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string str = "Translated Text: " + richTextBox2.Text;

                        File.WriteAllText(sfd.FileName, str);
                    }
                }

                else if (e.Result.Text == "clear")
                {
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    comboBox1.Text = string.Empty;
                    comboBox2.Text = string.Empty;
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mail m = new Mail(richTextBox1.Text);
            m.Show();
            this.Close();
        }

        private void narrateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Narrator nr = new Narrator(richTextBox1.Text);
            nr.Show();
            this.Close();
        }

        private void mailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Mail m = new Mail(richTextBox2.Text);
            m.Show();
            this.Close();
        }

        private void narrateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Narrator nr = new Narrator(richTextBox2.Text);
            nr.Show();
            this.Close();
        }
    }
}
