using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Speech.Recognition;

namespace Utility
{
    public partial class DataDeCryptor : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        string extsource = string.Empty;

        public DataDeCryptor()
        {
            InitializeComponent();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        }

        private void key()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Key and IV(*.dcki)|*.dcki";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string str = File.ReadAllText(ofd.FileName);
                string[] split = str.Split('\n');

                string key = split[0].ToString();
                string iv = split[1].ToString();

                textBox2.Text = key.Substring(5, key.Length - 6);
                textBox3.Text = iv.Substring(4);
            }
        }

        private void decrypt()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (extsource != string.Empty)
                {
                    ifexist();
                }

                else
                {
                    try
                    {
                        con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Project of Final Year\Project\Utility\bin\Debug\Databases\data_cryptor.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
                        con.Open();

                        string query = string.Format("select text from data where [key]='{0}' AND iv='{1}'", textBox2.Text, textBox3.Text);
                        cmd = new SqlCommand(query, con);

                        dr = cmd.ExecuteReader();

                        string data = null;
                        while (dr.Read())
                        {
                            data = dr["text"].ToString();
                        }

                        byte[] dkey = Convert.FromBase64String(textBox2.Text);
                        byte[] div = Convert.FromBase64String(textBox3.Text);
                        byte[] text = Convert.FromBase64String(data);

                        AesCryptoServiceProvider daes = new AesCryptoServiceProvider();
                        ICryptoTransform dict = daes.CreateDecryptor(dkey, div);
                        MemoryStream dms = new MemoryStream(text);
                        CryptoStream dcs = new CryptoStream(dms, dict, CryptoStreamMode.Read);
                        StreamReader dsr = new StreamReader(dcs);

                        richTextBox1.Text = dsr.ReadToEnd();

                        dsr.Close();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
        }

        private void browse()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                extsource = File.ReadAllText(ofd.FileName);

                MessageBox.Show("Encrypted Text Loaded...\nProvide 'Key' and 'IV' then click DeCrypt to view the decrypted text", "Success");
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox1.Clear();
        }

        private void ifexist()
        {
            byte[] dkey = Convert.FromBase64String(textBox2.Text);
            byte[] div = Convert.FromBase64String(textBox3.Text);
            byte[] text = Convert.FromBase64String(extsource);

            AesCryptoServiceProvider daes = new AesCryptoServiceProvider();
            ICryptoTransform dict = daes.CreateDecryptor(dkey, div);
            MemoryStream dms = new MemoryStream(text);
            CryptoStream dcs = new CryptoStream(dms, dict, CryptoStreamMode.Read);
            StreamReader dsr = new StreamReader(dcs);

            richTextBox1.Text = dsr.ReadToEnd();
        }

        private void DataDeCryptor_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "decrypt", "browse", "key", "clear", "close" });

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
                if (e.Result.Text == "decrypt")
                {
                    decrypt();
                }

                else if (e.Result.Text == "browse")
                {
                    browse();
                }

                else if (e.Result.Text == "key")
                {
                    key();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
