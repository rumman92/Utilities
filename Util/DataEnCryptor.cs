using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Speech.Recognition;

namespace Utility
{
    public partial class DataEnCryptor : Form
    {
        SqlConnection con;
        SqlCommand cmd;

        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        byte[] key;
        byte[] iv;
        byte[] etext;

        public DataEnCryptor()
        {
            InitializeComponent();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            key = aes.Key;
            iv = aes.IV;
        }

        private void clear()
        {
            richTextBox1.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void encyptedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void save()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Key and IV(*.dcki)|*.dcki";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string str = "Key: " + textBox2.Text + Environment.NewLine + "IV: " + textBox3.Text;
                File.WriteAllText(sfd.FileName, str);
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void browse()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                richTextBox1.Text = File.ReadAllText(ofd.FileName);
            }
        }

        private void generate()
        {
            textBox2.Text = Convert.ToBase64String(key);
            textBox3.Text = Convert.ToBase64String(iv);
        }

        private void encrypt()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (richTextBox1.Text == string.Empty)
            {
                MessageBox.Show("No Data found, please enter some data", "Faliure");
            }

            else
            {

                try
                {
                    con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Project of Final Year\Project\Utility\bin\Debug\Databases\data_cryptor.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
                    con.Open();

                    byte[] ekey = Convert.FromBase64String(textBox2.Text);
                    byte[] eiv = Convert.FromBase64String(textBox3.Text);

                    AesCryptoServiceProvider eaes = new AesCryptoServiceProvider();
                    ICryptoTransform eict = eaes.CreateEncryptor(ekey, eiv);
                    MemoryStream ems = new MemoryStream();
                    CryptoStream ecs = new CryptoStream(ems, eict, CryptoStreamMode.Write);
                    StreamWriter esw = new StreamWriter(ecs);
                    esw.Write(richTextBox1.Text);
                    esw.Close();
                    etext = ems.ToArray();

                    string str = Convert.ToBase64String(etext);

                    string query = string.Format("insert into data values ('{0}','{1}','{2}')", textBox2.Text, textBox3.Text, str);
                    cmd = new SqlCommand(query, con);

                    cmd.ExecuteNonQuery();

                    richTextBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();

                    MessageBox.Show("Crypted", "Success");
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

        private void DataEnCryptor_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "encrypt", "browse", "save", "clear", "text", "close" });

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
                if (e.Result.Text == "encrypt")
                {
                    encrypt();
                }

                else if (e.Result.Text == "browse")
                {
                    browse();
                }

                else if (e.Result.Text == "save")
                {
                    save();
                }

                else if (e.Result.Text == "clear")
                {
                    clear();
                }

                else if (e.Result.Text == "text")
                {
                    EText et = new EText();
                    et.ShowDialog();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name == "DataEnCryptor")
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

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
