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
    public partial class MBAIssue : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        public MBAIssue()
        {
            InitializeComponent();

            try
            {
                con = new SqlConnection(@"Data Source=RUMMAN92;Initial Catalog=MBA;Integrated Security=True");
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void MBAIssue_Load(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != string.Empty)
                {
                    string query = string.Format("select [Book Name] from MBADetails where [Stream]='{0}'", comboBox1.Text);
                    da = new SqlDataAdapter(query, con);

                    ds.Clear();
                    da.Fill(ds, "MBADetails");
                    foreach (DataRow dr in ds.Tables["MBADetails"].Rows)
                    {
                        listBox1.Items.Add(dr["Book Name"].ToString());
                    }
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

                else if (e.Result.Text == "close" && ActiveForm.Name == "MBAIssue")
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = string.Format("select * from MBADetails where [Book Name] ='{0}'", listBox1.SelectedItem.ToString());
                da = new SqlDataAdapter(query, con);

                ds.Clear();
                da.Fill(ds, "MBADetails");
                foreach (DataRow dr in ds.Tables["MBADetails"].Rows)
                {
                    textBox1.Text = dr["Book Name"].ToString();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();

                clear();

                string query = string.Format("select [Book Name] from MBADetails where Stream ='{0}'", comboBox1.Text);
                da = new SqlDataAdapter(query, con);

                ds.Clear();
                da.Fill(ds, "MBADetails");
                foreach (DataRow dr in ds.Tables["MBADetails"].Rows)
                {
                    listBox1.Items.Add(dr["Book Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void search()
        {
            try
            {
                if (radioButton1.Checked)
                {

                    string query = string.Format("select * from MBADetails where [Book Name]='{0}'", textBox1.Text);
                    da = new SqlDataAdapter(query, con);

                    called();

                    textBox1.ReadOnly = true;
                    radioButton1.Checked = false;
                }

                else if (radioButton2.Checked)
                {
                    string query = string.Format("select * from MBADetails where [Author]='{0}'", textBox2.Text);
                    da = new SqlDataAdapter(query, con);

                    called();

                    textBox2.ReadOnly = true;
                    radioButton2.Checked = false;
                }

                else if (radioButton3.Checked)
                {
                    string query = string.Format("select * from MBADetails where [ID Number]={0}", textBox3.Text);
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

        private void called()
        {
            try
            {
                ds.Clear();
                da.Fill(ds, "MBADetails");

                foreach (DataRow dr in ds.Tables["MBADetails"].Rows)
                {
                    textBox1.Text = dr["Book Name"].ToString();
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

        private void confirm()
        {
            if (textBox3.Text != string.Empty)
            {
                MBAIssueConfirm mbaissueconfirm = new MBAIssueConfirm(textBox3.Text, comboBox1.Text);
                mbaissueconfirm.Show();
            }
            else
            {
                MessageBox.Show("Book Id cannot be empty", "Error");
            }
        }
    }
}
