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
    public partial class BArchAddBook : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        
        SqlConnection con;
        SqlDataAdapter da = new SqlDataAdapter();

        public BArchAddBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void append()
        {
            try
            {
                con = new SqlConnection(@"Data Source=RUMMAN92\SQLEXPRESS;Initial Catalog=BArch;Integrated Security=True");
                con.Open();

                string query = string.Format("insert into BArchDetails values('{0}','{1}','{2}','{3}','{4}','{5}')", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, Convert.ToInt32(textBox6.Text));
                da.InsertCommand = new SqlCommand(query, con);
                da.InsertCommand.ExecuteNonQuery();

                MessageBox.Show("Book Added Successfully", "Added");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void BArchAddBook_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "append", "clear", "close" });

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
                if (e.Result.Text == "append")
                {
                    append();
                }

                else if (e.Result.Text == "clear")
                {
                    clear();
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
