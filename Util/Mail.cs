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
using WindowsFormsToolkit.Controls;
using System.Speech.Recognition;

namespace Utility
{
    public partial class Mail : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public Mail()
        {
            InitializeComponent();
        }

        public Mail(string str)
        {
            InitializeComponent();
            richTextBox1.Text = str;
        }

        private void send()
        {
            if (textBox1.Text == string.Empty)
            {
                errorProvider1.SetError(textBox1, "Specify To Column Value");
            }
            else if (textBox2.Text == string.Empty)
            {
                errorProvider1.SetError(textBox2, "Specify Subject Column Value");
            }
            else if (textBox4.Text == string.Empty)
            {
                errorProvider1.SetError(textBox4, "Specify Valid User Name");
            }
            else if (textBox5.Text == string.Empty)
            {
                errorProvider1.SetError(textBox5, "Specify Valid Password");
            }

            else
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    string to = textBox1.Text;

                    File.AppendAllText(Environment.CurrentDirectory + @"\list.txt", to + " ");

                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                    message.To.Add(textBox1.Text);
                    message.From = new System.Net.Mail.MailAddress(textBox4.Text);
                    message.Subject = textBox2.Text;
                    message.Body = richTextBox1.Text;
                    message.IsBodyHtml = true;

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

                    smtp.Credentials = new NetworkCredential(textBox4.Text, textBox5.Text);

                    if ((textBox3.Text) != string.Empty)
                    {
                        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(openFileDialog1.FileName);
                        message.Attachments.Add(attach);
                    }

                    switch (comboBox1.Text)
                    {
                        case "Gmail": smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            break;
                        case "Hotmail": smtp.Host = "smtp.live.com";
                            smtp.Port = 587;
                            break;
                        case "Yahoo": smtp.Host = "smtp.mail.yahoo.com";
                            smtp.Port = 995;
                            break;
                        default: errorProvider1.SetError(comboBox1, "Select an account");
                            break;
                    }

                    smtp.EnableSsl = true;
                    smtp.Timeout = 0;
                    smtp.Send(message);

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Sent", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Faliure");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void narrateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Narrator nr = new Narrator(richTextBox1.Text);
            nr.Show();
            this.Close();
        }

        private void translateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Translator tr = new Translator(richTextBox1.Text);
            tr.Show();
            this.Close();
        }

        private void Mail_Load(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory+@"\list.txt"))
            {
                string to = File.ReadAllText(Environment.CurrentDirectory+@"\list.txt");
                listBox1.Items.AddRange(to.Split(' '));
            }

            CueTextExtender cue = new CueTextExtender();
            cue.SetCueText(textBox1, "Enter receiver's email address");
            cue.SetCueText(textBox2, "Enter subject for the email");
            cue.SetCueText(textBox4, "Email address");
            cue.SetCueText(textBox5, "Password");

            Choices ch = new Choices();
            ch.Add(new string[] { "send", "save", "attach", "clear", "close" });

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
                if (e.Result.Text == "send")
                {
                    send();
                }

                else if (e.Result.Text == "save")
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Text Files(*.txt)|*.txt";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string str;
                        str = "To: " + textBox1.Text + Environment.NewLine;
                        str += "From: " + textBox4.Text + Environment.NewLine;
                        str += "Subject: " + textBox2.Text + Environment.NewLine;
                        str += "Attachment: " + textBox3.Text + Environment.NewLine;
                        str += "Message Body: " + richTextBox1.Text;
                        File.WriteAllText(sfd.FileName, str);
                    }   
                }

                else if (e.Result.Text == "attach")
                {
                    openFileDialog1.Filter = "All Files(*.*)|*.*";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        textBox3.Text = openFileDialog1.FileName;
                    }
                }

                else if (e.Result.Text == "clear")
                {
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    richTextBox1.Clear();
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }
    }
}
