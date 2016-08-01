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
    public partial class BTechIssueConfirm : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        SqlConnection con;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        string stream = string.Empty;

        public BTechIssueConfirm()
        {
            InitializeComponent();
        }

        public BTechIssueConfirm(string id, string sstream)
        {
            InitializeComponent();
            textBox3.Text = id;

            stream = sstream;
        }

        private void release()
        {
            try
            {
                con = new SqlConnection(@"Data Source=RUMMAN92;Initial Catalog=BTech;Integrated Security=True");
                con.Open();

                string insert = string.Format("insert into BTechIssueConfirm values('{0}','{1}','{2}','{3}','{4}')", textBox1.Text, textBox2.Text, textBox3.Text, DateTime.Now.ToString(), stream);

                da.InsertCommand = new SqlCommand(insert, con);
                da.InsertCommand.ExecuteNonQuery();

                string status = string.Format("select [Number of Copies] from BTechDetails where [ID Number]={0}", textBox3.Text);
                da = new SqlDataAdapter(status, con);

                da.Fill(ds, "BTechDetails");

                int number = 0;
                foreach (DataRow dr in ds.Tables["BTechDetails"].Rows)
                {
                    number = Convert.ToInt32(dr["Number of Copies"]);
                }

                number = number - 1;

                string insertcopies = string.Format("update BTechDetails set [Number of Copies]={0} where [ID Number]={1}", number, textBox3.Text);
                da.UpdateCommand = new SqlCommand(insertcopies, con);
                da.UpdateCommand.ExecuteNonQuery();

                MessageBox.Show("Book Issued to : " + textBox1.Text, "Success");

                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void BTechIssueConfirm_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "release", "clear", "close" });

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
                if (e.Result.Text == "release")
                {
                    release();
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
