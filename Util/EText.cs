using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Speech.Recognition;

namespace Utility
{
    public partial class EText : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader dr;

        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public EText()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void view()
        {
            try
            {
                con = new SqlConnection(@"Data Source=PHENOMENAL\SQLEXPRESS;Initial Catalog=data_cryptor;Integrated Security=True");
                con.Open();

                string query = string.Format("select text from data where [key]='{0}'", textBox1.Text);
                cmd = new SqlCommand(query, con);

                dr = cmd.ExecuteReader();

                string data = null;
                while (dr.Read())
                {
                    data = dr["text"].ToString();
                }

                richTextBox1.Text = data;

                if (richTextBox1.Text == "")
                {
                    MessageBox.Show("Invalid key","Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
            finally
            {
                con.Close();
            }
        }

        private void save()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files(*.txt)|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, richTextBox1.Text);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mail m = new Mail(richTextBox1.Text);
            m.Show();
            this.Close();
        }

        private void EText_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "save", "view", "close" });

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
                if (e.Result.Text == "view")
                {
                    view();
                }

                else if (e.Result.Text == "save")
                {
                    save();
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
