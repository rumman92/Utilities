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
    public partial class MArchIssue : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        public MArchIssue()
        {
            InitializeComponent();
            
            try
            {
                con = new SqlConnection(@"Data Source=RUMMAN92;Initial Catalog=MArch;Integrated Security=True");
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format("select * from MArchDetails where Name='{0}'", listBox1.SelectedItem.ToString());
                da = new SqlDataAdapter(query, con);

                called();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MArchIssue_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "M.Arch";

            try
            {
                string query = string.Format("select * from MArchDetails");
                da = new SqlDataAdapter(query, con);

                da.Fill(ds, "MArchDetails");

                foreach (DataRow dr in ds.Tables["MArchDetails"].Rows)
                {
                    listBox1.Items.Add(dr["Name"].ToString());
                }

                Choices ch = new Choices();
                ch.Add(new string[] { "search", "confirm", "close" });

                GrammarBuilder gb = new GrammarBuilder(ch);
                Grammar g = new Grammar(gb);

                sre.LoadGrammarAsync(g);
                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (e.Result.Text == "confirm")
                {
                    confirm();
                }

                else if (e.Result.Text == "search")
                {
                    search();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "MArchIssue")
                {
                    this.Close();
                    sre.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void search()
        {
            try
            {
                if (radioButton1.Checked)
                {

                    string query = string.Format("select * from MArchDetails where Name='{0}'", textBox1.Text);
                    da = new SqlDataAdapter(query, con);

                    called();

                    textBox1.ReadOnly = true;
                    radioButton1.Checked = false;
                }

                else if (radioButton2.Checked)
                {
                    string query = string.Format("select * from MArchDetails where Author='{0}'", textBox2.Text);
                    da = new SqlDataAdapter(query, con);

                    called();

                    textBox2.ReadOnly = true;
                    radioButton2.Checked = false;
                }

                else if (radioButton3.Checked)
                {
                    string query = string.Format("select * from MArchDetails where [ID Number]={0}", textBox3.Text);
                    da = new SqlDataAdapter(query, con);

                    called();

                    textBox3.ReadOnly = true;
                    radioButton3.Checked = false;
                }

                else
                {
                    MessageBox.Show("Select any method to search", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void called()
        {
            try
            {
                da.Fill(ds, "MArchDetails");

                foreach (DataRow dr in ds.Tables["MArchDetails"].Rows)
                {
                    textBox1.Text = dr["Name"].ToString();
                    textBox2.Text = dr["Author"].ToString();
                    textBox3.Text = dr["ID Number"].ToString();
                    textBox4.Text = dr["Genre"].ToString();
                    textBox5.Text = dr["Publisher"].ToString();
                    textBox6.Text = dr["Number of Copies"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.ReadOnly = false;

                if (textBox2.ReadOnly == false || textBox3.ReadOnly == false)
                {
                    textBox2.Clear();
                    textBox2.ReadOnly = true;

                    textBox3.Clear();
                    textBox3.ReadOnly = true;
                }
            }
            else
            {
                textBox1.ReadOnly = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox2.ReadOnly = false;

                if (textBox1.ReadOnly == false || textBox3.ReadOnly == false)
                {
                    textBox1.Clear();
                    textBox1.ReadOnly = true;

                    textBox3.Clear();
                    textBox3.ReadOnly = true;
                }
            }
            else
            {
                textBox2.ReadOnly = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox3.ReadOnly = false;

                if (textBox1.ReadOnly == false || textBox2.ReadOnly == false)
                {
                    textBox1.Clear();
                    textBox1.ReadOnly = true;

                    textBox2.Clear();
                    textBox2.ReadOnly = true;
                }
            }
            else
            {
                textBox3.ReadOnly = true;
            }
        }

        private void confirm()
        {
            try
            {
                if (textBox3.Text != string.Empty)
                {
                    MArchIssueConfirm marchissueconfirm = new MArchIssueConfirm(textBox3.Text);
                    marchissueconfirm.Show();
                }
                else
                    MessageBox.Show("Book Id cannot be empty", "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
