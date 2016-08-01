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
using WindowsFormsToolkit.Controls;

namespace Utility
{
    public partial class ImageDeCryptor : Form
    {
        SqlConnection cond;
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        SpeechRecognitionEngine sre=new SpeechRecognitionEngine();

        public ImageDeCryptor()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browse()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Text Files(*.txt)|*.txt";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = ofd.FileName;
                    richTextBox1.Text = File.ReadAllText(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
        }

        private void decrypt()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                cond = new SqlConnection(@"Data Source=RUMMAN92;Initial Catalog=image_cryptor;Integrated Security=True");
                cond.Open();

                string str = textBox2.Text;

                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
                data = md5.ComputeHash(data);
                string text = System.Text.Encoding.ASCII.GetString(data);

                string query = string.Format("select text from image where [key]='{0}'", text);
                da = new SqlDataAdapter(query, cond);

                da.Fill(ds, "image");

                foreach(DataRow dr in ds.Tables["image"].Rows)
                {
                    string rev = dr["text"].ToString();
                    char[] ch = rev.ToCharArray();
                    Array.Reverse(ch);
                    string ans = new string(ch);
                    richTextBox1.Text = ans;
                }

                if (richTextBox1.Text == "")
                {
                    MessageBox.Show("Key not valid, Try again", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
            finally
            {
                cond.Close();
            }
        }

        private void generate()
        {
            try
            {
                byte[] bt = Convert.FromBase64String(richTextBox1.Text);
                MemoryStream ms = new MemoryStream(bt, 0, bt.Length);

                ms.Write(bt, 0, bt.Length);

                Image img = Image.FromStream(ms);
                pictureBox1.Image = img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void ImageDeCryptor_Load(object sender, EventArgs e)
        {
            CueTextExtender cue = new CueTextExtender();
            cue.SetCueText(textBox2, "speak 'key' to load key, then say 'decrypt' and finally say 'generate'");

            Choices ch = new Choices();
            ch.Add(new string[] { "decrypt", "browse", "generate", "key", "save","text","image", "close" });

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

                else if (e.Result.Text == "save")
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Image);
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Image File(*.bmp)|*.bmp";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        bmp.Save(sfd.FileName);
                    }
                }

                else if (e.Result.Text == "generate")
                {
                    generate();
                }

                else if (e.Result.Text == "key")
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Key(*.ick)|*.ick";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        textBox2.Text = File.ReadAllText(ofd.FileName);
                    }
                }

                else if (e.Result.Text == "text")
                {
                    tabControl1.SelectTab(tabPage1);
                }

                else if (e.Result.Text == "image")
                {
                    tabControl1.SelectTab(tabPage2);
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
