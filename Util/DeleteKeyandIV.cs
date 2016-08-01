using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Speech.Recognition;

namespace Utility
{
    public partial class DeleteKeyandIV : Form
    {
        SqlConnection con;
        SqlCommand cmd;

        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public DeleteKeyandIV()
        {
            InitializeComponent();
        }

        private void delete()
        {
            if (checkBox1.Checked)
            {
                try
                {
                    con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Project of Final Year\Project\Utility\bin\Debug\Databases\image_cryptor.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
                    con.Open();

                    string query = string.Format("truncate table image");

                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Formated", "Success");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
                    {
                        MessageBox.Show("Enter valid values", "Error!");
                    }
                    else
                    {
                        con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Project of Final Year\Project\Utility\bin\Debug\Databases\data_cryptor.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
                        con.Open();

                        string query = string.Format("delete from data where [key]='{0}' AND iv='{1}'", textBox1.Text, textBox2.Text);
                        cmd = new SqlCommand(query, con);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Deleted", "Success");
                        this.Close();
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
        }

        private void DeleteKeyandIV_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "delete", "close" });

            GrammarBuilder gb = new GrammarBuilder(ch);
            Grammar g = new Grammar(gb);

            sre.LoadGrammarAsync(g);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "delete")
            {
                delete();
            }

            else if (e.Result.Text == "close")
            {
                this.Close();
                sre.Dispose();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
        }
    }
}
